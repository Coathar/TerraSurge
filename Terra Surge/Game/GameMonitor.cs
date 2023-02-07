using System.Collections.Concurrent;
using IronOcr;
using Newtonsoft.Json;
using SharpDX.Text;
using SharpPcap;
using TerraSurgeShared.Models;
using TerraSurge.Utilities;
using AForge.Imaging;

namespace TerraSurge.Game
{
    public class GameMonitor
    {
        private Thread RunningThread { get; set; }

        private TerraSurge TerraSurge { get; set; }

        private ICaptureDevice CaptureDevice { get; set; }

        private bool captureDeviceRunning;
        private BNetPlayer bnetPlayer;
        private IGameState currentGameState;
        private string currentMapName;

        private ConcurrentQueue<Bitmap> ImagesToProcess { get; set; }

        public GameMonitor(TerraSurge terraSurge, ICaptureDevice captureDevice)
        {
            TerraSurge = terraSurge;
            CaptureDevice = captureDevice;
            ImagesToProcess = new ConcurrentQueue<Bitmap>();
            currentGameState = GameStateFactory.CreateGameState(GameState.InMenu, this, TerraSurge);
        }

        public void Start()
        {
            RunningThread = new Thread(new ThreadStart(ThreadRun));
            RunningThread.Start();
        }

        public void QueueImage(Bitmap bitmap)
        {
            ImagesToProcess.Enqueue(bitmap);
        }

        private void ThreadRun()
        {
            int i = 0;
            while (!TerraSurge.CTS.IsCancellationRequested)
            {
                if (bnetPlayer == null && !captureDeviceRunning)
                {
                    captureDeviceRunning = true;
                    TryCaptureLocalPlayerPacket();
                }

                if (ImagesToProcess.TryDequeue(out Bitmap nextImage))
                {
                    TerraSurge.SetProcessingPreview((Bitmap)nextImage.Clone());
                    currentGameState.ProcessImage(nextImage);
                    nextImage.Dispose();
                }
            }

            CaptureDevice.StopCapture();
        }

        public void SetGameState(GameState gameState)
        {
            currentGameState = GameStateFactory.CreateGameState(gameState, this, TerraSurge);
        }

        public bool HasMapNameSet()
        {
            return !string.IsNullOrEmpty(currentMapName);
        }

        public void SetMapName(string mapName)
        {
            currentMapName = mapName;
        }
      
        private void TryCaptureLocalPlayerPacket()
        {
            CaptureDevice.OnPacketArrival += new PacketArrivalEventHandler(CaptureDevicePacketArrival);

            int readTimeoutMilliseconds = 1000;
            CaptureDevice.Open(mode: DeviceModes.Promiscuous | DeviceModes.DataTransferUdp | DeviceModes.NoCaptureLocal, read_timeout: readTimeoutMilliseconds);

            CaptureDevice.Filter = "udp src port 4242";

            CaptureDevice.StartCapture();
        }

        private void CaptureDevicePacketArrival(object sender, PacketCapture e)
        {
            try
            {
                NearbyPlayer player = JsonConvert.DeserializeObject<NearbyPlayer>(Encoding.ASCII.GetString(e.GetPacket().GetPacket().PayloadPacket.PayloadPacket.PayloadData));
                
                if (player != null)
                {
                    long accountID = Convert.ToInt64(player.account.Replace(":0100000000000000", ""), 16);
                    BNetPlayer dbPlayer = TerraSurge.DBContext.BNetPlayer.Where(x => x.AccountID == accountID).FirstOrDefault();

                    if (dbPlayer == null)
                    {
                        dbPlayer = new BNetPlayer()
                        {
                            AccountID = accountID,
                        };

                        bool invalidUsername = true;

                        while (invalidUsername)
                        {
                            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter Username with tag (Krusher99#1234)", "Enter Username");

                            if (!input.Contains("#"))
                                continue;

                            invalidUsername = false;

                            dbPlayer.DisplayName = input.Split("#")[0];
                            dbPlayer.Tag = input.Split("#")[1];
                        }

                        TerraSurge.DBContext.BNetPlayer.Add(dbPlayer);
                        TerraSurge.DBContext.SaveChangesAsync();
                    }

                    bnetPlayer = dbPlayer;
                    TerraSurge.SetCurrentPlayer(dbPlayer);

                    CaptureDevice.StopCapture();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private class NearbyPlayer
        {
            public string version { get; set; }

            public string build { get; set; }

            public string account { get; set; }

            public string bnet_account { get; set; }

            public long avatar { get; set; }

            public int level { get; set; }

            public long pframe { get; set; }

            public long title { get; set; }

            public long name_card { get; set; }

            public int platform { get; set; }

            public int elevel { get; set; }
        }
    }
}

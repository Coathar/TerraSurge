using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using IronOcr;
using SharpPcap;
using TerraSurge.Game;
using TerraSurge.ScreenWatcher;
using TerraSurgeShared;
using TerraSurgeShared.Models;

namespace TerraSurge
{
    public partial class TerraSurge : Form
    {
        private bool ScreenWatcherRunning { get; set; } = false;

        private ScreenWatcher.ScreenWatcher ActiveWatcher { get; set; }

        public GameMonitor GameMonitor { get; set; }

        public CancellationTokenSource CTS { get; set; }

        public AppDbContext DBContext { get; set; }

        private Dictionary<string, string> CachedProcesses { get; set; } = new Dictionary<string, string>();

        private Dictionary<string, ICaptureDevice> CachedNetworkDevices { get; set; } = new Dictionary<string, ICaptureDevice>();

        public TerraSurge()
        {
            InitializeComponent();
        }

        private void TerraSurge_Load(object sender, EventArgs e)
        {
            DBContext = new AppDbContext();
        }

        private void DebugRunLoaders_Click(object sender, EventArgs e)
        {
            DBContext.RunLoaders();
        }

        private void DebugRecreateDB_Click(object sender, EventArgs e)
        {
            DBContext.Database.EnsureDeleted();
            DBContext.Database.EnsureCreated();
        }

        private void StartStopBtn_Click(object sender, EventArgs e)
        {
            if (!ScreenWatcherRunning)
            {
                CTS = new CancellationTokenSource();

                Process[] foundProcesses = Process.GetProcessesByName(CachedProcesses[ApplicationComboBox.Text]);

                if (foundProcesses.Length > 0)
                {
                    ActiveWatcher = new ScreenWatcher.ScreenWatcher(foundProcesses[0], this);
                    ActiveWatcher.Start();

                    GameMonitor = new GameMonitor(this, CachedNetworkDevices[NetworkDeviceComboBox.Text]);
                    GameMonitor.Start();
                }
                else
                {
                    MessageBox.Show("Invalid process selected. Please try selecting the process again.");
                    return;
                }
            }
            else
            {
                CTS.Cancel();
            }


            ScreenWatcherRunning = !ScreenWatcherRunning;
            StartStopBtn.Text = ScreenWatcherRunning ? "Stop" : "Start";
        }

        private void ApplicationComboBox_DropDown(object sender, EventArgs e)
        {
            ApplicationComboBox.Items.Clear();
            CachedProcesses.Clear();

            List<string> toShow = new List<string>();

            Process[] processes = Process.GetProcesses();

            foreach (Process p in processes)
            {
                if (!string.IsNullOrEmpty(p.MainWindowTitle))
                {
                    string fileName = string.Empty;

                    // Assemble file name and if it doesn't exist, skip this process.
                    try
                    {
                        fileName = $" - ({Path.GetFileName(p?.MainModule?.FileName)})";
                    }
                    catch (Win32Exception)
                    { }

                    // Append window title with file name.
                    string displayName = $"{p.MainWindowTitle}{fileName}";

                    // Removes duplicate programs that are marked with a (1). Weird workaround, will need to redo later
                    if (CachedProcesses.ContainsKey(displayName))
                    {
                        displayName += "(1)";
                    }

                    toShow.Add(displayName);
                    CachedProcesses.Add(displayName, p.ProcessName);
                }
            }

            ApplicationComboBox.Items.Clear();
            ApplicationComboBox.Items.AddRange(toShow.OrderBy(s => s).ToArray());

            int maxWidth = 0, temp = 0;

            foreach (var obj in ApplicationComboBox.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), ApplicationComboBox.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            ApplicationComboBox.DropDownWidth = maxWidth;
        }

        public void SetPreview(Bitmap bitmap)
        {
            if (PreviewPictureBox.InvokeRequired)
            {
                PreviewPictureBox.Invoke(SetPreview, bitmap);
            }
            else
            {
                if (PreviewPictureBox.Image != null)
                    PreviewPictureBox.Image.Dispose();

                PreviewPictureBox.Image = bitmap;
            }
        }

        public void SetCurrentPlayer(BNetPlayer player)
        {
            if (CurrentPlayerDisplayLbl.InvokeRequired)
            {
                CurrentPlayerDisplayLbl.Invoke(SetCurrentPlayer, player);
            }
            else
            {
                CurrentPlayerDisplayLbl.Text = $"{player.DisplayName}#{player.Tag} ({player.AccountID})";
            }
        }

        private void NetworkDeviceComboBox_DropDown(object sender, EventArgs e)
        {
            NetworkDeviceComboBox.Items.Clear();

            CaptureDeviceList networkDevices = CaptureDeviceList.Instance;

            if (networkDevices.Count == 0)
            {
                MessageBox.Show(text: "No devices found! Make sure WinPcap is installed.");
                return;
            }

            List<string> toShow = new List<string>();

            foreach (ICaptureDevice device in networkDevices)
            {
                toShow.Add($"{device.Description}");
                CachedNetworkDevices[device.Description] = device;
            }

            NetworkDeviceComboBox.Items.Clear();
            NetworkDeviceComboBox.Items.AddRange(toShow.OrderBy(s => s).ToArray());

            int maxWidth = 0, temp = 0;

            foreach (var obj in NetworkDeviceComboBox.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), NetworkDeviceComboBox.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            NetworkDeviceComboBox.DropDownWidth = maxWidth;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string testImage = "C:\\Users\\train\\Documents\\ShareX\\Screenshots\\2023-01\\Overwatch_8gX8sv7YWA.jpg";
        }
    }
}
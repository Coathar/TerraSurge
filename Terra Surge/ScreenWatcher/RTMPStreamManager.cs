using System.Diagnostics;
using OpenCvSharp;
using TerraSurge.Utilities;
using OpenCvSharp.Extensions;

namespace TerraSurge.ScreenWatcher
{
    public class RTMPStreamManager : IDisposable
    {
        private VideoCapture CaptureDevice { get; set; }

        private TerraSurge TerraSurge { get; set; }

        private Thread RunningThread { get; set; }


        private string ip;


        public RTMPStreamManager(string ip, TerraSurge terraSurge)
        {
            this.ip = ip;
            CaptureDevice = new VideoCapture();
            TerraSurge = terraSurge;
        }

        public void Start()
        {
            CaptureDevice.BufferSize = 3;
            CaptureDevice.Open(ip);

            RunningThread = new Thread(new ThreadStart(ThreadRun));
            RunningThread.Start();
        }


        private void ThreadRun()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (CaptureDevice.IsOpened() && !TerraSurge.CTS.IsCancellationRequested)
            {
                // Run at 30 times per second, for othe frames, discard to keep up.
                if (stopwatch.ElapsedMilliseconds > 33)
                {
                    stopwatch.Restart();

                    using (Mat mat = new Mat())
                    {
                        if (CaptureDevice.Read(mat))
                        {
                            using (Bitmap capturedFrame = BitmapConverter.ToBitmap(mat))
                            {
                                using (Bitmap resized = WindowUtilities.ScaleToHD(capturedFrame))
                                {
                                    TerraSurge.SetCapturePreview((Bitmap)resized.Clone());
                                    TerraSurge.GameMonitor.QueueImage((Bitmap)resized.Clone());
                                }
                            }
                        }
                    }
                }
                else
                {
                    CaptureDevice.Grab();
                }
            }

            Dispose();
        }

        public void Dispose()
        {
            CaptureDevice.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TerraSurge.Utilities;

namespace TerraSurge.ScreenWatcher
{
    /// <summary>
    /// Used to run the thread for the window capture.
    /// All window capture code is adapted from here: https://github.com/MathewSachin/Captura
    /// </summary>
    public class ScreenWatcher
    {
        private Process TargetProcess { get; set; }

        private WindowProvider WindowProvider { get; set; }

        private TerraSurge TerraSurge { get; set; }

        private Thread RunningThread { get; set; }

        public ScreenWatcher(Process process, TerraSurge terraSurge)
        {
            TargetProcess = process;
            TerraSurge = terraSurge;
        }

        public void Start()
        {
            RunningThread = new Thread(new ThreadStart(ThreadRun));
            RunningThread.Start();
        }

        private void ThreadRun()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            WindowProvider = new WindowProvider(new Window(TargetProcess.MainWindowHandle));
            int i = 0;
            while (!TargetProcess.HasExited && !TerraSurge.CTS.IsCancellationRequested)
            {
                // Run at 30 times per second
                if (stopwatch.ElapsedMilliseconds > 150)
                {
                    stopwatch.Restart();

                    using (IBitmapFrame frame = WindowProvider.Capture().GenerateFrame())
                    {
                        // Frame is a weird width, re-try getting the window because it was likely a full screen
                        // application that was minimized. This is a horrible hacky way to do this. 
                        if (frame.Width < 200)
                        {
                            WindowProvider.Dispose();
                            WindowProvider = new WindowProvider(new Window(TargetProcess.MainWindowHandle));
                            continue;
                        }

                        using (Bitmap bitmap = new Bitmap(frame.Width, frame.Height))
                        {
                            BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, new Size(frame.Width, frame.Height)), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                            try
                            {
                                frame.CopyTo(data.Scan0);
                            }
                            catch (Exception e)
                            {

                            }
                            finally
                            {
                                bitmap.UnlockBits(data);
                            }

                            // Scale down output
                            Bitmap resized = WindowUtilities.ScaleToHD(bitmap);
                            TerraSurge.SetCapturePreview((Bitmap)resized.Clone());
                            TerraSurge.GameMonitor.QueueImage((Bitmap)resized.Clone());
                            resized.Dispose();
                        }
                    }
                }
            }

            WindowProvider.Dispose();
        }        
    }
}

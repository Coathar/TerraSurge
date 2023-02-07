using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using IronOcr;
using SharpPcap;
using TerraSurge.Utilities;

namespace TerraSurge.Game
{
    public class InGameState : IGameState
    {
        private GameMonitor gameMonitor;
        private TerraSurge terraSurge;
        private ExhaustiveTemplateMatching templateMatching;
        private bool initialTabPush;
        private SerialPort serialPort;
        private IronTesseract ocr;
        private Stopwatch stopwatch;
        private long mapFoundTime;

        public InGameState(GameMonitor gameMonitor, TerraSurge terraSurge)
        {
            this.gameMonitor = gameMonitor;
            this.terraSurge = terraSurge;

            templateMatching = new ExhaustiveTemplateMatching();

            ocr = new IronTesseract();
            ocr.MultiThreaded = false;
            ocr.Language = OcrLanguage.EnglishFast;

            ocr.Configuration.ReadBarCodes = false;
            ocr.Configuration.RenderSearchablePdfsAndHocr = false;

            serialPort = new SerialPort(terraSurge.GetSerialPort(), 9600);
            serialPort.RtsEnable = true;
            serialPort.DtrEnable = true;
            serialPort.Open();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void ProcessImage(Bitmap toProcess)
        {
            // Wait 65 seconds before initiating a tab push to get map name and starting hero.
            // 65 seconds is select hero + assembly phase
            if (stopwatch.ElapsedMilliseconds > 65000 && !initialTabPush)
            {
                CheckForMapName();
            }

            if (initialTabPush)
            {
                using (Bitmap cropped = WindowUtilities.CropImage(toProcess, WindowUtilities.RectangleDefinitions.MapName))
                {
                    using (OcrInput ocrInput = new OcrInput())
                    {
                        ocrInput.Add(cropped);

                        OcrResult result = ocr.Read(ocrInput);

                        string processedMapName = WindowUtilities.ConvertScoreboardText(result.Text);

                        if (!string.IsNullOrEmpty(processedMapName))
                        {
                            gameMonitor.SetMapName(processedMapName);

                            StopTab();
                            initialTabPush = false;
                            mapFoundTime = stopwatch.ElapsedMilliseconds;
                        }
                    }
                }
            }
        }

        private void CheckForMapName()
        {
            initialTabPush = true;

            SendTab();
        }

        private void SendTab()
        {
            serialPort.Write("1");
        }

        private void StopTab()
        {
            serialPort.Write("0");
        }
    }
}

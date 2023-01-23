using System.Drawing;
using IronOcr;
using TerraSurgeShared;

namespace TerraSurge.Test
{
    [TestClass]
    public class OCRTests
    {
        [TestMethod]
        public void TestMapLoadingScreens()
        {
            IronTesseract ocr = new IronTesseract();

            string resourceFolder = @"Resources\MapLoadingScreens\";
            string[] loadingScreens = Directory.GetFiles(resourceFolder);

            foreach (string fileName in loadingScreens)
            {
                string targetMapName = fileName.Split("_")[0].Replace(resourceFolder, string.Empty);

                Image cropped = Util.Util.CropImage(new Bitmap(fileName), new Rectangle(1700, 1270, 650, 90));
                cropped.Save("toProcess.png");
                string codeToRun = OcrInputFilterWizard.Run("toProcess.png", out double confidence, ocr);

                File.WriteAllText($"code_{targetMapName}.txt", codeToRun);
                /*
                using (OcrInput input = new OcrInput())
                {
                    input.Add(cropped);
                    //input.Binarize();
                    //input.SelectTextColor(IronSoftware.Drawing.Color.White);
                    //input.Deskew();
                    //input.Contrast();
                    //input.DeNoise();

                    //input.Invert();
                    //input.Erode();
                    //input.AdaptiveThreshold();
                    //input.Close();

                    input.SaveAsImages("temp");

                    

                    OcrResult result = ocr.Read(input);
                    // Assert.IsTrue(result.Text.ToLower().Contains(targetMapName.ToLower()), $"{result.Text} did not contain {targetMapName}");
                }
                */
                cropped.Dispose();
            }
        }
    }
}
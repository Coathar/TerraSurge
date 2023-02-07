using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography.X509Certificates;
using AForge.Imaging;
using IronOcr;
using SharpDX.Direct3D11;
using TerraSurge.Game;
using TerraSurge.Utilities;
using TerraSurgeShared;
using TerraSurgeShared.Enums;

namespace TerraSurge.Test
{
    [TestClass]
    public class ScreenTests
    {
        public static TemplateManager templateManager;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            templateManager = new TemplateManager();
            templateManager.LoadTemplates();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            templateManager.Dispose();
        }

        [TestMethod]
        public void TestMapScoreboardScreens()
        {
            IronTesseract ocr = new IronTesseract();

            string resourceFolder = @"Resources\MapScoreboardScreens\";
            string[] loadingScreens = Directory.GetFiles(resourceFolder);

            foreach (string fileName in loadingScreens)
            {
                string targetMapName = fileName.Replace(".png", "").Replace(resourceFolder, "");

                Bitmap resourceImage = new Bitmap(fileName);

                using (Bitmap cropped = WindowUtilities.CropImage(resourceImage, WindowUtilities.RectangleDefinitions.MapName, true))
                {
                    cropped.Save(targetMapName + "_cropped.png");
                    cropped.Save("toProcess.png");

                    //string codeToRun = OcrInputFilterWizard.Run("toProcess.png", out double confidence, ocr);

                    // File.WriteAllText(targetMapName + ".txt", codeToRun);

                    using (OcrInput input = new OcrInput())
                    {
                        input.Add(cropped);

                        OcrResult result = ocr.Read(input);
                        result.SaveAsTextFile(targetMapName + "_result.txt");
                        Assert.IsTrue(string.Equals(WindowUtilities.ConvertScoreboardText(result.Text), targetMapName, StringComparison.OrdinalIgnoreCase), $"{result.Text} was not {targetMapName}");
                    }
                }
            }
        }

        [TestMethod]
        public void TestJoiningGame()
        {
            Bitmap gameFound = WindowUtilities.ConvertToFormat(
                WindowUtilities.CropImage(new Bitmap(@"Resources\CompGameFound.png"), WindowUtilities.RectangleDefinitions.JoiningGame, true),
                PixelFormat.Format24bppRgb, true);

            Bitmap inQueue = WindowUtilities.ConvertToFormat(
                WindowUtilities.CropImage(new Bitmap(@"Resources\InQueue.png"), WindowUtilities.RectangleDefinitions.JoiningGame, true),
                PixelFormat.Format24bppRgb, true);

            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching();

            TemplateMatch[] gameFoundMatching = tm.ProcessImage(gameFound, templateManager.GetTemplate(Template.ConnectingToServer));

            Assert.AreEqual(1, gameFoundMatching.Length);
            Assert.AreEqual(1, gameFoundMatching[0].Similarity);

            TemplateMatch[] inQueueMatching = tm.ProcessImage(inQueue, templateManager.GetTemplate(Template.ConnectingToServer));

            Assert.AreEqual(0, inQueueMatching.Length);
        }

        [TestMethod]
        public void TestHeroScoreboard()
        {
            foreach (Hero hero in Enum.GetValues<Hero>())
            {
                Bitmap template = WindowUtilities.ScaleImage(templateManager.GetHeroIconTemplate(hero), 171);

                Bitmap scoreboardImage = WindowUtilities.ConvertToFormat(
                    WindowUtilities.CropImage(new Bitmap(@"Resources\HeroScoreboard\" + hero.ToString() + ".png"), WindowUtilities.RectangleDefinitions.HeroScoreboard, true),
                    PixelFormat.Format24bppRgb, true);

                template.Save(hero + "_template.png");
                scoreboardImage.Save(hero + ".png");

                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching();

                TemplateMatch[] gameFoundMatching = tm.ProcessImage(scoreboardImage, template);

                Assert.AreEqual(1, gameFoundMatching.Length, $"Template does not match scoreboard for hero: {hero}");
                Assert.AreEqual(1, gameFoundMatching[0].Similarity, 0.1, $"Template does not match scoreboard for hero: {hero}");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using TerraSurge.Utilities;

namespace TerraSurge.Game
{
    public class MenuGameState : IGameState
    {
        private GameMonitor gameMonitor;
        private TerraSurge terraSurge;
        private ExhaustiveTemplateMatching templateMatching;

        public MenuGameState(GameMonitor gameMonitor, TerraSurge terraSurge)
        {
            this.gameMonitor = gameMonitor;
            this.terraSurge = terraSurge;
            this.templateMatching = new ExhaustiveTemplateMatching();
        }

        public void ProcessImage(Bitmap toProcess)
        {
            Bitmap cropped = WindowUtilities.CropImage(toProcess, WindowUtilities.RectangleDefinitions.JoiningGame);

            cropped = WindowUtilities.ConvertToFormat(cropped, System.Drawing.Imaging.PixelFormat.Format24bppRgb, true);

            TemplateMatch[] matches = templateMatching.ProcessImage(cropped, terraSurge.TemplateManager.GetTemplate(Template.ConnectingToServer));

            if (matches.Length > 0 && matches[0].Similarity >= 0.9d)
            {
                gameMonitor.SetGameState(GameState.InGame);
            }

            cropped.Dispose();
        }
    }
}

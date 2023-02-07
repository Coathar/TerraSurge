using TerraSurge.Extensions;
using TerraSurge.Utilities;
using TerraSurgeShared.Enums;

namespace TerraSurge.Game
{
    public class TemplateManager : IDisposable
    {
        Dictionary<Template, Bitmap> LoadedTemplates = new Dictionary<Template, Bitmap>();

        Dictionary<Hero, Bitmap> LoadedHeroIcons = new Dictionary<Hero, Bitmap>();

        public TemplateManager()
        {

        }

        public Bitmap GetTemplate(Template template)
        {
            return LoadedTemplates[template];
        }

        public Bitmap GetHeroIconTemplate(Hero hero)
        {
            return LoadedHeroIcons[hero];
        }

        public void Dispose()
        {
            foreach (Bitmap b in LoadedTemplates.Values)
            {
                b.Dispose();
            }

            foreach (Bitmap b in LoadedHeroIcons.Values)
            {
                b.Dispose();
            }
        }

        public void LoadTemplates()
        {
            foreach (Template template in Enum.GetValues<Template>())
            {
                LoadedTemplates.Add(template,
                    WindowUtilities.ConvertToFormat(new Bitmap(@"Resources\ImageTemplates\" + template.ToString() + ".png"), System.Drawing.Imaging.PixelFormat.Format24bppRgb, true));
            }

            foreach (Hero hero in Enum.GetValues<Hero>())
            {
                Bitmap temp = WindowUtilities.ReplaceAlphaWithColor(new Bitmap(@"Resources\ImageTemplates\HeroIcons\" + hero.ToString() + ".png"), 59, 70, 90);

                temp.Save(hero + "_test.png");

                LoadedHeroIcons.Add(hero,
                    WindowUtilities.ConvertToFormat(temp, System.Drawing.Imaging.PixelFormat.Format24bppRgb, true));
            }
        }
    }

    public enum Template
    {
        ConnectingToServer,

    }
}

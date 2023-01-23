namespace TerraSurge.Util
{
    public static class Util
    {
        public static Bitmap CropImage(Bitmap image, Rectangle cropRectangle)
        {
            Bitmap target = new Bitmap(cropRectangle.Width, cropRectangle.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(image, -cropRectangle.X, -cropRectangle.Y);

                return target;
            }
        }

        public static string OCRMapNameToMapName(string ocrInput)
        {
            switch(ocrInput)
            {
                case "— BLIZZARD WORLD":
                    return "Blizzard World";
            }

            throw new ArgumentException($"Unknown map with OCR text: {ocrInput}");
        }
    }
}

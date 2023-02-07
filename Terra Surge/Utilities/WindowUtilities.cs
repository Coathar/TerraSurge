using System.Drawing.Imaging;

namespace TerraSurge.Utilities
{
    public static class WindowUtilities
    {

        #region Rectangle Definitions
        public static class RectangleDefinitions
        {
            public static Rectangle JoiningGame = new Rectangle(895, 30, 200, 45);

            public static Rectangle MapName = new Rectangle(120, 30, 570, 30);

            public static Rectangle HeroScoreboard = new Rectangle(1172, 161, 171, 171);
        }
        #endregion

        #region Text Definitions
        public static class TextDefinitions
        {
            public static string JoiningGame = "GAME FOUND!\r\nCONNECTING TO SERVER";
        }
        #endregion

        public static Bitmap CropImage(Bitmap image, Rectangle cropRectangle, bool disposeOriginal = false)
        {
            Bitmap target = new Bitmap(cropRectangle.Width, cropRectangle.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(image, -cropRectangle.X, -cropRectangle.Y);

                if (disposeOriginal)
                    image.Dispose();

                return target;
            }
        }

        /// <summary>
        /// Scales the given bitmap to 1080p. It will dispose of the bitmap provided and return a new image.
        /// </summary>
        /// <param name="image">The bitmap to resize.</param>
        /// <returns>The resized bitmap.</returns>
        public static Bitmap ScaleToHD(Bitmap image)
        {
            return ScaleImage(image, 1080);
        }

        public static Bitmap ScaleImage(Bitmap image, int height)
        {
            double ratio = (double)height / image.Height;
            int newWidth = (int)(image.Width * ratio);
            Bitmap newImage = new Bitmap(newWidth, height, image.PixelFormat);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, height);
            }

            image.Dispose();
            return newImage;
        }

        public static Bitmap ConvertToFormat(Image image, PixelFormat format, bool disposeOriginal = false)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);

            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }

            if (disposeOriginal)
                image.Dispose();

            return copy;
        }

        public static string ConvertScoreboardText(string input)
        {
            string[] split = input.Split("|");

            if (split.Length > 1)
            {
                return split[1].Trim();
            }    

            return string.Empty;
        }

        public static Bitmap ReplaceAlphaWithColor(Bitmap input, int r, int g, int b)
        {
            if (input.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("Not supported PNG image!");

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, input.Width, input.Height);
            BitmapData bmpData = input.LockBits(rect, ImageLockMode.ReadWrite, input.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * input.Height;
            byte[] rgbaValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbaValues, 0, bytes);

            // array consists of values RGBARGBARGBA

            for (int counter = 0; counter < rgbaValues.Length; counter += 4)
            {
                double t = rgbaValues[counter + 3] / 255.0; // transparency of pixel between 0 .. 1 , easier to do math with this
                double rt = 1 - t; // inverted value of transparency

                // C = C * t + W * (1-t) // alpha transparency for your case C-color, W-white (255)
                // same for each color
                rgbaValues[counter] = (byte)(rgbaValues[counter] * t + r * rt); // R color
                rgbaValues[counter + 1] = (byte)(rgbaValues[counter + 1] * t + g * rt); // G color
                rgbaValues[counter + 2] = (byte)(rgbaValues[counter + 2] * t + b * rt); // B color

                rgbaValues[counter + 3] = 255; // A = 255 => no transparency 
            }
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbaValues, 0, ptr, bytes);

            // Unlock the bits.
            input.UnlockBits(bmpData);

            return input;
        }
    }
}

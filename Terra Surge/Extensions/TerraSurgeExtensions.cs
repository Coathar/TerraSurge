using System.Text;
using TerraSurge.Utilities;

namespace TerraSurge.Extensions
{
    public static class TerraSurgeExtensions
    {
        public static Rectangle ToRectangle(this Rect r)
        {
            return Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);
        }

        public static Rectangle Even(this Rectangle Rect)
        {
            if (Rect.Width % 2 == 1)
                --Rect.Width;

            if (Rect.Height % 2 == 1)
                --Rect.Height;

            return Rect;
        }

        public static string ConvertUnderscoreWord(this Enum toConvert)
        {
            return ConvertUnderscoreWord(toConvert.ToString());
        }

        public static string ConvertUnderscoreWord(this string input)
        {
            string[] split = input.Split("_");
            StringBuilder toReturn = new StringBuilder();
            for (int i = 0; i < split.Length; i++)
            {
                if (!string.IsNullOrEmpty(split[i]))
                    toReturn.Append(split[i].FirstCharToUpper());
            }

            return toReturn.ToString();
        }

        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };
    }
}

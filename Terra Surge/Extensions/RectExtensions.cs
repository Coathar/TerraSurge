using TerraSurge.Util;

namespace TerraSurge.Extensions
{
    public static class RectExtensions
    {
        public static Rectangle ToRectangle(this Rect r)
        {
            return Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);
        }
    }
}

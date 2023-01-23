using System.Runtime.InteropServices;

namespace TerraSurge.Util
{
    static class Gdi32
    {
        const string DllName = "gdi32.dll";

        [DllImport(DllName)]
        public static extern bool StretchBlt(IntPtr hObject, int XDest, int YDest, int WDest, int HDest, IntPtr ObjectSource, int XSrc, int YSrc, int WSrc, int HSrc, int Op);
    }
}

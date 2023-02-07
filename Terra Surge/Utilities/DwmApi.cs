using System.Runtime.InteropServices;

namespace TerraSurge.Utilities
{
    public static class DwmApi
    {
        const string DllName = "dwmapi.dll";

        [DllImport(DllName)]
        public static extern int DwmGetWindowAttribute(IntPtr Window, int Attribute, out bool Value, int Size);

        [DllImport(DllName)]
        public static extern int DwmGetWindowAttribute(IntPtr Window, int Attribute, ref Rect Value, int Size);
    }
}

using System.Runtime.InteropServices;

namespace TerraSurge.Util
{
    public class Kernel32
    {
        const string DllName = "Kernel32";

        [DllImport(DllName, EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory(IntPtr Dest, IntPtr Src, int Count);
    }
}

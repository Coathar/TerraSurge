using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using TerraSurge.Utilities;

namespace TerraSurge.ScreenWatcher
{
    public class Texture2DFrame : IBitmapFrame
    {
        public Texture2D Texture { get; }
        public Texture2D PreviewTexture { get; }

        public Device Device { get; }

        public Texture2DFrame(Texture2D Texture,
            Device Device,
            Texture2D PreviewTexture)
        {
            this.Texture = Texture;
            this.Device = Device;
            this.PreviewTexture = PreviewTexture;

            var desc = Texture.Description;

            Width = desc.Width;
            Height = desc.Height;
        }

        Texture2DFrame() { }

        public static IBitmapFrame DummyFrame { get; } = new Texture2DFrame();

        public void Dispose() { }

        public int Width { get; }
        public int Height { get; }

        public void CopyTo(byte[] Buffer)
        {
            var mapSource = Device.ImmediateContext.MapSubresource(Texture, 0, MapMode.Read, MapFlags.None);

            var destStride = Width * 4;

            try
            {
                // Do not copy directly, strides may be different
                Parallel.For(0, Height, Y =>
                {
                    Marshal.Copy(mapSource.DataPointer + Y * mapSource.RowPitch,
                        Buffer,
                        Y * destStride,
                        destStride);
                });
            }
            finally
            {
                Device.ImmediateContext.UnmapSubresource(Texture, 0);
            }
        }

        public void CopyTo(IntPtr Buffer)
        {
            var mapSource = Device.ImmediateContext.MapSubresource(Texture, 0, MapMode.Read, MapFlags.None);

            var destStride = Width * 4;

            try
            {
                // Do not copy directly, strides may be different
                Parallel.For(0, Height, Y =>
                {
                    Kernel32.CopyMemory(Buffer + Y * destStride,
                        mapSource.DataPointer + Y * mapSource.RowPitch,
                        destStride);
                });
            }
            finally
            {
                Device.ImmediateContext.UnmapSubresource(Texture, 0);
            }
        }
    }
}

namespace TerraSurge.ScreenWatcher
{
    public class RepeatFrame : IBitmapFrame, IEditableFrame
    {
        RepeatFrame() { }

        public static RepeatFrame Instance { get; } = new RepeatFrame();

        IBitmapFrame IEditableFrame.GenerateFrame() => Instance;

        int IBitmapFrame.Width { get; } = -1;

        float IEditableFrame.Height { get; } = -1;

        float IEditableFrame.Width { get; } = -1;

        int IBitmapFrame.Height { get; } = -1;

        void IDisposable.Dispose() { }

        void IBitmapFrame.CopyTo(byte[] Buffer)
        {
            throw new NotImplementedException();
        }

        void IBitmapFrame.CopyTo(IntPtr Buffer)
        {
            throw new NotImplementedException();
        }
    }
}

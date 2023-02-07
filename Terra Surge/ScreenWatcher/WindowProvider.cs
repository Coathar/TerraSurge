using TerraSurge.Exceptions;
using TerraSurge.Extensions;
using TerraSurge.ScreenWatcher;
using TerraSurge.Utilities;

namespace TerraSurge.ScreenWatcher
{
    /// <summary>
    /// Captures the specified window.
    /// </summary>
    class WindowProvider
    {
        readonly Window _window;

        readonly IntPtr _hdcSrc;
        readonly DxgiTargetDeviceContext _dcTarget;

        static Func<Point, Point> GetTransformer(Window Window)
        {
            var initialSize = Window.Rectangle.Even().Size;

            return P =>
            {
                var rect = Window.Rectangle;

                var ratio = Math.Min((float)initialSize.Width / rect.Width, (float)initialSize.Height / rect.Height);

                return new Point((int)((P.X - rect.X) * ratio), (int)((P.Y - rect.Y) * ratio));
            };
        }

        /// <summary>
        /// Creates a new instance of <see cref="WindowProvider"/>.
        /// </summary>
        public WindowProvider(Window Window)
        {
            _window = Window ?? throw new ArgumentNullException(nameof(Window));

            var size = Window.Rectangle.Even().Size;
            Width = size.Width;
            Height = size.Height;

            PointTransform = GetTransformer(Window);

            _hdcSrc = User32.GetDC(IntPtr.Zero);

            _dcTarget = new DxgiTargetDeviceContext(Width, Height);
        }

        public Func<Point, Point> PointTransform { get; }

        void OnCapture()
        {
            if (!_window.IsAlive)
            {
                throw new WindowClosedException();
            }
            IntPtr hdcDest = _dcTarget.GetDC();

            Rectangle rect = _window.Rectangle.Even();
            float ratio = Math.Min((float)Width / rect.Width, (float)Height / rect.Height);

            int resizeWidth = (int)(rect.Width * ratio);
            int resizeHeight = (int)(rect.Height * ratio);


            void ClearRect(Rect Rect)
            {
                User32.FillRect(hdcDest, ref Rect, IntPtr.Zero);
            }

            if (Width != resizeWidth)
            {
                ClearRect(new Rect
                {
                    Left = resizeWidth,
                    Right = Width,
                    Bottom = Height
                });
            }
            else if (Height != resizeHeight)
            {
                ClearRect(new Rect
                {
                    Top = resizeHeight,
                    Right = Width,
                    Bottom = Height
                });
            }

            Gdi32.StretchBlt(hdcDest, 0, 0, resizeWidth, resizeHeight,
                _hdcSrc, rect.X, rect.Y, rect.Width, rect.Height,
                (int)CopyPixelOperation.SourceCopy);
        }

        public IEditableFrame Capture()
        {
            try
            {
                OnCapture();

                IEditableFrame img = _dcTarget.GetEditableFrame();

                return img;
            }
            catch (Exception e) when (!(e is WindowClosedException))
            {
                return RepeatFrame.Instance;
            }
        }

        /// <summary>
        /// Height of Captured image.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Width of Captured image.
        /// </summary>
        public int Width { get; }

        public void Dispose()
        {
            _dcTarget.Dispose();
            
            User32.ReleaseDC(IntPtr.Zero, _hdcSrc);
        }

        public IBitmapFrame DummyFrame => _dcTarget.DummyFrame;
    }
}

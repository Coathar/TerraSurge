namespace TerraSurge.ScreenWatcher
{
    public class Direct2DEditor : IEditableFrame
    {
        readonly Direct2DEditorSession _editorSession;

        public Direct2DEditor(Direct2DEditorSession EditorSession)
        {
            _editorSession = EditorSession;

            var desc = EditorSession.StagingTexture.Description;

            Width = desc.Width;
            Height = desc.Height;

            EditorSession.BeginDraw();
        }

        public void Dispose() { }

        public float Width { get; }
        public float Height { get; }

        public IBitmapFrame GenerateFrame()
        {
            _editorSession.EndDraw();

            return new Texture2DFrame(_editorSession.StagingTexture,
                _editorSession.Device,
                _editorSession.PreviewTexture);
        }
    }
}

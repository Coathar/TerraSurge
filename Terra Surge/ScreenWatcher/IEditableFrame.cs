namespace TerraSurge.ScreenWatcher
{
    public interface IEditableFrame : IDisposable
    {
        float Width { get; }
        float Height { get; }

        IBitmapFrame GenerateFrame();
    }
}
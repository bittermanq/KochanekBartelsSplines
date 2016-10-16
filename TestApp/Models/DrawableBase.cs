namespace KochanekBartelsSplines.TestApp.Models
{
    public abstract class DrawableBase
    {
        internal RedrawCallback RedrawCallback;

        internal void Redraw() => RedrawCallback?.Invoke();
    }
}
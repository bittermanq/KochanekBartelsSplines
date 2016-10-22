using System;

namespace KochanekBartelsSplines.TestApp.Models
{
    public abstract class DrawableBase
    {
        internal Action RedrawCallback;

        internal void Redraw() => RedrawCallback?.Invoke();
    }
}
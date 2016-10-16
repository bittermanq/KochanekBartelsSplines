using System.Windows.Media.Imaging;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Drawing.Interfaces
{
    internal interface IBitmapDrawer
    {
        WriteableBitmap GetBitmap(BitmapChannel bitmapChannel, GraphicsGetter graphicsGetter);
    }
}
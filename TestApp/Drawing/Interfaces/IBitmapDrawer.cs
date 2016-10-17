using System.Windows.Media.Imaging;
using KochanekBartelsSplines.TestApp.Models.Interfaces;

namespace KochanekBartelsSplines.TestApp.Drawing.Interfaces
{
    internal interface IBitmapDrawer
    {
        WriteableBitmap GetBitmap(IBitmapChannel bitmapChannel, IGraphicsGetter graphicsGetter);
    }
}
using System.Windows.Media.Imaging;
using KochanekBartelsSplines.Models;

namespace KochanekBartelsSplines.Drawing.Interfaces
{
    internal interface IBitmapDrawer
    {
        WriteableBitmap GetBitmap(BitmapChannel bitmapChannel);
    }
}
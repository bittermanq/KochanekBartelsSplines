using KochanekBartelsSplines.TestApp.Drawing.Interfaces;

namespace KochanekBartelsSplines.TestApp.Drawing
{
    internal class GraphicsGetter : IGraphicsGetter
    {
        public System.Drawing.Graphics FromImage(System.Drawing.Bitmap bitmap)
        {
            return System.Drawing.Graphics.FromImage(bitmap);
        }
    }
}

using System.Drawing;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class ColorContainer: DrawableBase
    {
        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value; 
                Redraw();
            }
        }


        public ColorContainer(Color color, RedrawCallback redrawCallback)
        {
            _color = color;
            RedrawCallback = redrawCallback;
        }
    }
}

using System.Drawing;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class AnchorPoint: DrawableBase
    {
        public bool IsActive { get; set; }

        private Point _position;
        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Redraw();
            }
        }

        private double _tension;
        public double Tension
        {
            get { return _tension; }
            set
            {
                _tension = value;
                Redraw();
            }
        }

        private double _continuity;
        public double Continuity
        {
            get { return _continuity; }
            set
            {
                _continuity = value;
                Redraw();
            }
        }
        
        private double _bias;
        public double Bias
        {
            get { return _bias; }
            set
            {
                _bias = value;
                Redraw();
            }
        }


        public AnchorPoint(Point position, bool isActive, RedrawCallback redrawCallback)
        {
            RedrawCallback = redrawCallback;

            Position = position;
            IsActive = isActive;
        }
    }
}
using System.Collections.Generic;
using System.Drawing;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class Curve: DrawableBase
    {
        public List<Point> Points { get; private set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }

        private readonly ColorContainer _colorContainer;
        public Color Color
        {
            get { return _colorContainer.Color; }
            set { _colorContainer.Color = value; }
        }

        public string Name => $"Curve {Id}";


        public Curve(ColorContainer color)
        {
            Points = new List<Point>();
            _colorContainer = color;
            
        }
    }
}
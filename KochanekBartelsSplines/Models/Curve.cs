using System.Collections.Generic;
using System.Drawing;

namespace KochanekBartelsSplines.Models
{
    public class Curve
    {
        public List<Point> Points { get; private set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public string Name => $"Curve {Id}";

        public Curve()
        {
            Points = new List<Point>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;

namespace KochanekBartelsSplines.Models
{
    public class Curve
    {
        public List<Point> Points { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public string Name
        {
            get { return String.Format("Curve {0}", Id); }
        }

        public Curve()
        {
            Points = new List<Point>();
        }
    }
}
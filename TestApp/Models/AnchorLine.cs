using System.Collections.Generic;
using System.Drawing;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class AnchorLine
    {
        public List<AnchorPoint> Points { get; private set; }
        public bool IsClosed { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }

        public ColorContainer ColorContainer { get; set; }

        public Color Color
        {
            get { return ColorContainer.Color; }
            set { ColorContainer.Color = value; }
        }


        public AnchorLine(ColorContainer colorContainer)
        {
            Points = new List<AnchorPoint>();
            ColorContainer = colorContainer;
        }
    }
}
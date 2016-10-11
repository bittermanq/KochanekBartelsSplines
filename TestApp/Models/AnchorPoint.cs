using System.Drawing;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class AnchorPoint
    {
        public Point Position { get; set; }
        public bool IsActive { get; set; }

        public AnchorPoint(Point position, bool isActive)
        {
            Position = position;
            IsActive = isActive;
        }
    }
}
using System.Collections.Generic;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class AnchorLine
    {
        public List<AnchorPoint> Points { get; set; }
        public bool IsClosed { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }

        public AnchorLine()
        {
            Points = new List<AnchorPoint>();
        }
    }
}
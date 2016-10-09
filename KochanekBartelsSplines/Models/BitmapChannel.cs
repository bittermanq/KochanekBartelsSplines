using System.Collections.Generic;

namespace KochanekBartelsSplines.Models
{
    public class BitmapChannel
    {
        public List<AnchorLine> AnchorLines { get; set; }
        public List<Curve> Curves { get; set; }


        public BitmapChannel()
        {
            AnchorLines = new List<AnchorLine>();
            Curves = new List<Curve>();
        }
    }
}
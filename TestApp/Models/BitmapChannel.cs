using System.Collections.Generic;
using KochanekBartelsSplines.TestApp.Models.Interfaces;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class BitmapChannel : IBitmapChannel
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
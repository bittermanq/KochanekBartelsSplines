using System.Collections.Generic;

namespace KochanekBartelsSplines.TestApp.Models.Interfaces
{
    public interface IBitmapChannel
    {
        List<AnchorLine> AnchorLines { get; set; }
        List<Curve> Curves { get; set; }
    }
}
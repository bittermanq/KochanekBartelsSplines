using System.Collections.Generic;
using KochanekBartelsSplines.Models;

namespace KochanekBartelsSplines.Helpers.Interfaces
{
    public interface ILineInterpolator
    {
        List<Curve> GetCurves(IEnumerable<AnchorLine> anchorLines, double tension, double continuity, double bias, int steps);
    }
}
using System.Collections.Generic;
using System.Drawing;
using KochanekBartelsSplines.Models;

namespace KochanekBartelsSplines.Helpers.Interfaces
{
    public interface IInterpolatedPointsCalculator
    {
        List<Point> GetInterpolatedPoints(AnchorPoint point1, AnchorPoint point2, AnchorPoint point3, AnchorPoint point4, double tension, double continuity, double bias, int steps);
    }
}
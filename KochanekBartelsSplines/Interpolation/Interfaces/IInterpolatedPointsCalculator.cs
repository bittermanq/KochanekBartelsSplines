using System.Collections.Generic;
using System.Drawing;

namespace KochanekBartelsSplines.Interpolation.Interfaces
{
    public interface IInterpolatedPointsCalculator
    {
        List<Point> GetInterpolatedPoints(Point point1, Point point2, Point point3, Point point4, double tension, double continuity, double bias, int steps);
    }
}
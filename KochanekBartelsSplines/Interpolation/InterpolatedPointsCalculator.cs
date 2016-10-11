using System;
using System.Collections.Generic;
using System.Drawing;
using KochanekBartelsSplines.Interpolation.Interfaces;

namespace KochanekBartelsSplines.Interpolation
{
    public class InterpolatedPointsCalculator : IInterpolatedPointsCalculator
    {
        public List<Point> GetInterpolatedPoints(Point point1, Point point2, Point point3, Point point4, double tension, double continuity, double bias, int steps)
        {
            var interpolatedPoints = new List<Point>();

            for (var i = 0; i < steps; i++)
            {
                var s = i/(double) steps;
                
                var h1 = 2*Math.Pow(s, 3) - 3*Math.Pow(s, 2) + 1;
                var h2 = (-2)*Math.Pow(s, 3) + 3*Math.Pow(s, 2);
                var h3 = Math.Pow(s, 3) - 2*Math.Pow(s, 2) + s;
                var h4 = Math.Pow(s, 3) - Math.Pow(s, 2);

                var tDix = (1 - tension) * (1 + continuity) * (1 + bias) * (point2.X - point1.X) / 2 + (1 - tension) * (1 - continuity) * (1 - bias) * (point3.X - point2.X) / 2;
                var tDiy = (1 - tension) * (1 + continuity) * (1 + bias) * (point2.Y - point1.Y) / 2 + (1 - tension) * (1 - continuity) * (1 - bias) * (point3.Y - point2.Y) / 2;

                var tSix = (1 - tension) * (1 - continuity) * (1 + bias) * (point3.X - point2.X) / 2 + (1 - tension) * (1 + continuity) * (1 - bias) * (point4.X - point3.X) / 2;
                var tSiy = (1 - tension) * (1 - continuity) * (1 + bias) * (point3.Y - point2.Y) / 2 + (1 - tension) * (1 + continuity) * (1 - bias) * (point4.Y - point3.Y) / 2;

                var x = h1 * point2.X + h2 * point3.X + h3 * tDix + h4 * tSix;
                var y = h1 * point2.Y + h2 * point3.Y + h3 * tDiy + h4 * tSiy;

                interpolatedPoints.Add(new Point((int) x, (int) y));
            }

            interpolatedPoints.Add(point3);

            return interpolatedPoints;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using KochanekBartelsSplines.Helpers.Interfaces;
using KochanekBartelsSplines.Models;

namespace KochanekBartelsSplines.Helpers
{
    public class InterpolatedPointsCalculator : IInterpolatedPointsCalculator
    {
        public List<Point> GetInterpolatedPoints(AnchorPoint point1, AnchorPoint point2, AnchorPoint point3, AnchorPoint point4, double tension, double continuity, double bias, int steps)
        {
            var interpolatedPoints = new List<Point>();

            for (var i = 0; i < steps; i++)
            {
                var s = i/(double) steps;
                
                var h1 = 2*Math.Pow(s, 3) - 3*Math.Pow(s, 2) + 1;
                var h2 = (-2)*Math.Pow(s, 3) + 3*Math.Pow(s, 2);
                var h3 = Math.Pow(s, 3) - 2*Math.Pow(s, 2) + s;
                var h4 = Math.Pow(s, 3) - Math.Pow(s, 2);

                var tDix = (1 - tension) * (1 + continuity) * (1 + bias) * (point2.Position.X - point1.Position.X) / 2 + (1 - tension) * (1 - continuity) * (1 - bias) * (point3.Position.X - point2.Position.X) / 2;
                var tDiy = (1 - tension) * (1 + continuity) * (1 + bias) * (point2.Position.Y - point1.Position.Y) / 2 + (1 - tension) * (1 - continuity) * (1 - bias) * (point3.Position.Y - point2.Position.Y) / 2;

                var tSix = (1 - tension) * (1 - continuity) * (1 + bias) * (point3.Position.X - point2.Position.X) / 2 + (1 - tension) * (1 + continuity) * (1 - bias) * (point4.Position.X - point3.Position.X) / 2;
                var tSiy = (1 - tension) * (1 - continuity) * (1 + bias) * (point3.Position.Y - point2.Position.Y) / 2 + (1 - tension) * (1 + continuity) * (1 - bias) * (point4.Position.Y - point3.Position.Y) / 2;

                var x = h1 * point2.Position.X + h2 * point3.Position.X + h3 * tDix + h4 * tSix;
                var y = h1 * point2.Position.Y + h2 * point3.Position.Y + h3 * tDiy + h4 * tSiy;

                interpolatedPoints.Add(new Point((int) x, (int) y));
            }

            interpolatedPoints.Add(point3.Position);

            return interpolatedPoints;
        }
    }
}

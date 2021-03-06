using System;
using System.Drawing;
using System.Linq;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers
{
    public class SelectedAnchorPointGetter : ISelectedAnchorPointGetter
    {
        private const double SelectionRadius = 10;

        public AnchorPoint Get(Point point, AnchorLine anchorLine)
        {
            return anchorLine.Points.FirstOrDefault(anchorPoint => Math.Sqrt(Math.Pow(point.X - anchorPoint.Position.X, 2) + Math.Pow(point.Y - anchorPoint.Position.Y, 2)) <= SelectionRadius);
        }
    }
}
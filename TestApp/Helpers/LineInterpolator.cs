using System.Collections.Generic;
using KochanekBartelsSplines.Interpolation.Interfaces;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers
{
    public class LineInterpolator : ILineInterpolator
    {
        private readonly IInterpolatedPointsCalculator _pointsCalculator;

        public LineInterpolator(IInterpolatedPointsCalculator pointsCalculator)
        {
            _pointsCalculator = pointsCalculator;
        }
        
        public IEnumerable<Curve> GetCurves(IEnumerable<AnchorLine> anchorLines, ISplineSettingsController splineSettingsController)
        {
            var curves = new List<Curve>();

            var steps = splineSettingsController.Segments;

            foreach (var anchorLine in anchorLines)
            {
                var curve = new Curve(anchorLine.ColorContainer) { Id = anchorLine.Id, IsActive = anchorLine.IsActive };

                if (anchorLine.Points.Count >= 3) 
                {   
                    for (var i = 0; i < anchorLine.Points.Count; i++)
                    {
                        if (i == 0)
                        {
                            var point1 = anchorLine.Points[anchorLine.IsClosed ? anchorLine.Points.Count - 1 : i];
                            var point2 = anchorLine.Points[i];
                            var point3 = anchorLine.Points[i + 1];
                            var point4 = anchorLine.Points[i + 2];

                            AddInterpolatedPoints(curve, point1, point2, point3, point4, steps);
                        }
                        else if (i + 2 < anchorLine.Points.Count)
                        {
                            var point1 = anchorLine.Points[i - 1];
                            var point2 = anchorLine.Points[i];
                            var point3 = anchorLine.Points[i + 1];
                            var point4 = anchorLine.Points[i + 2];

                            AddInterpolatedPoints(curve, point1, point2, point3, point4, steps);
                        }
                        else if (i + 1 < anchorLine.Points.Count)
                        {
                            var point1 = anchorLine.Points[i - 1];
                            var point2 = anchorLine.Points[i];
                            var point3 = anchorLine.Points[i + 1];
                            var point4 = anchorLine.Points[anchorLine.IsClosed ? 0 : i + 1];

                            AddInterpolatedPoints(curve, point1, point2, point3, point4, steps);
                        }
                        else if (anchorLine.IsClosed)
                        {
                            var point1 = anchorLine.Points[i - 1];
                            var point2 = anchorLine.Points[i];
                            var point3 = anchorLine.Points[0];
                            var point4 = anchorLine.Points[1];

                            AddInterpolatedPoints(curve, point1, point2, point3, point4, steps);
                        }
                    }
                }

                curves.Add(curve);
            }

            return curves;
        }

        private void AddInterpolatedPoints(Curve curve, AnchorPoint point1, AnchorPoint point2, AnchorPoint point3, AnchorPoint point4, int steps)
        {
            var interpolatedPoints = _pointsCalculator.GetInterpolatedPoints(point1.Position, point2.Position, point3.Position, point4.Position, 
                                                                             point2.Tension, point2.Continuity, point2.Bias, steps);

            curve.Points.AddRange(interpolatedPoints);
        }
    }
}
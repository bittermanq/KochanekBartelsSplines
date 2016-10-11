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

            var tension = splineSettingsController.Tension;
            var bias = splineSettingsController.Bias;
            var continuity = splineSettingsController.Continuity;
            var steps = splineSettingsController.Segments;

            foreach (var anchorLine in anchorLines)
            {
                var curve = new Curve { Id = anchorLine.Id, IsActive = anchorLine.IsActive };

                if (anchorLine.Points.Count >= 3) 
                {   
                    for (var i = 0; i < anchorLine.Points.Count; i++)
                    {
                        if (i == 0)
                        {
                            var point1 = anchorLine.Points[anchorLine.IsClosed ? anchorLine.Points.Count - 1 : i].Position;
                            var point2 = anchorLine.Points[i].Position;
                            var point3 = anchorLine.Points[i + 1].Position;
                            var point4 = anchorLine.Points[i + 2].Position;

                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(point1, point2, point3, point4, tension, continuity, bias, steps));
                        }
                        else if (i + 2 < anchorLine.Points.Count)
                        {
                            var point1 = anchorLine.Points[i - 1].Position;
                            var point2 = anchorLine.Points[i].Position;
                            var point3 = anchorLine.Points[i + 1].Position;
                            var point4 = anchorLine.Points[i + 2].Position;

                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(point1, point2, point3, point4, tension, continuity, bias, steps));
                        }
                        else if (i + 1 < anchorLine.Points.Count)
                        {
                            var point1 = anchorLine.Points[i - 1].Position;
                            var point2 = anchorLine.Points[i].Position;
                            var point3 = anchorLine.Points[i + 1].Position;
                            var point4 = anchorLine.Points[anchorLine.IsClosed ? 0 : i + 1].Position;

                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(point1, point2, point3, point4, tension, continuity, bias, steps));
                        }
                        else if (anchorLine.IsClosed)
                        {
                            var point1 = anchorLine.Points[i - 1].Position;
                            var point2 = anchorLine.Points[i].Position;
                            var point3 = anchorLine.Points[0].Position;
                            var point4 = anchorLine.Points[1].Position;

                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(point1, point2, point3, point4, tension, continuity, bias, steps));
                        }
                    }
                }

                curves.Add(curve);
            }

            return curves;
        }
    }
}
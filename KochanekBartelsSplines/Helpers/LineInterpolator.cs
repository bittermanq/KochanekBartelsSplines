using System.Collections.Generic;
using KochanekBartelsSplines.Helpers.Interfaces;
using KochanekBartelsSplines.Models;

namespace KochanekBartelsSplines.Helpers
{
    internal class LineInterpolator : ILineInterpolator
    {
        private readonly IInterpolatedPointsCalculator _pointsCalculator;

        public LineInterpolator(IInterpolatedPointsCalculator pointsCalculator)
        {
            _pointsCalculator = pointsCalculator;
        }


        public List<Curve> GetCurves(IEnumerable<AnchorLine> anchorLines, double tension, double continuity, double bias, int steps)
        {
            var curves = new List<Curve>();

            foreach (var anchorLine in anchorLines)
            {
                var curve = new Curve { Id = anchorLine.Id, IsActive = anchorLine.IsActive };

                if (anchorLine.Points.Count >= 3) 
                {   
                    for (var i = 0; i < anchorLine.Points.Count; i++)
                    {
                        if (i == 0)
                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(anchorLine.Points[anchorLine.IsClosed ? anchorLine.Points.Count - 1 : i], anchorLine.Points[i], anchorLine.Points[i + 1], anchorLine.Points[i + 2], tension, continuity, bias, steps));
                        else if (i + 2 < anchorLine.Points.Count)
                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(anchorLine.Points[i - 1], anchorLine.Points[i], anchorLine.Points[i + 1], anchorLine.Points[i + 2], tension, continuity, bias, steps));
                        else if (i + 1 < anchorLine.Points.Count)
                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(anchorLine.Points[i - 1], anchorLine.Points[i], anchorLine.Points[i + 1], anchorLine.Points[anchorLine.IsClosed ? 0 : i + 1], tension, continuity, bias, steps));
                        else if(anchorLine.IsClosed)
                            curve.Points.AddRange(_pointsCalculator.GetInterpolatedPoints(anchorLine.Points[i - 1], anchorLine.Points[i], anchorLine.Points[0], anchorLine.Points[1], tension, continuity, bias, steps));
                    }
                }

                curves.Add(curve);
            }

            return curves;
        }
    }
}
using System.Collections.Generic;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers.Interfaces
{
    public interface ILineInterpolator
    {
        IEnumerable<Curve> GetCurves(IEnumerable<AnchorLine> anchorLines, ISplineSettingsController splineSettingsController);
    }
}
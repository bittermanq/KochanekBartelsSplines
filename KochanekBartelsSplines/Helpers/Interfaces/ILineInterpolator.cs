using System.Collections.Generic;
using KochanekBartelsSplines.Models;
using KochanekBartelsSplines.ViewModels.Interfaces;

namespace KochanekBartelsSplines.Helpers.Interfaces
{
    public interface ILineInterpolator
    {
        IEnumerable<Curve> GetCurves(IEnumerable<AnchorLine> anchorLines, ISplineSettingsController splineSettingsController);
    }
}
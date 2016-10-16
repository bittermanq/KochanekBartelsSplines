using KochanekBartelsSplines.TestApp.Helpers.Interfaces;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers
{
    public class SplineControllerFactory : ISplineControllerFactory
    {
        public ISplineSettingsController Get(RedrawCallback redrawCallback)
        {
            return new SplineSettingsController(redrawCallback);
        }
    }
}
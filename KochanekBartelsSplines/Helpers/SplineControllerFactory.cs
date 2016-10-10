using System;
using KochanekBartelsSplines.Helpers.Interfaces;

namespace KochanekBartelsSplines.Helpers
{
    public class SplineControllerFactory : ISplineControllerFactory
    {
        public ISplineSettingsController Get(Action redrawCallback)
        {
            return new SplineSettingsController(redrawCallback);
        }
    }
}
using System;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;

namespace KochanekBartelsSplines.TestApp.Helpers
{
    public class SplineControllerFactory : ISplineControllerFactory
    {
        public ISplineSettingsController Get(Action redrawCallback)
        {
            return new SplineSettingsController(redrawCallback);
        }
    }
}
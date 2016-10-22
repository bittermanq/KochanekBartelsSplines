using System;

namespace KochanekBartelsSplines.TestApp.Helpers.Interfaces
{
    public interface ISplineControllerFactory
    {
        ISplineSettingsController Get(Action redrawCallback);
    }
}
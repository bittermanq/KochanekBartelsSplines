using System;

namespace KochanekBartelsSplines.Helpers.Interfaces
{
    public interface ISplineControllerFactory
    {
        ISplineSettingsController Get(Action redrawCallback);
    }
}
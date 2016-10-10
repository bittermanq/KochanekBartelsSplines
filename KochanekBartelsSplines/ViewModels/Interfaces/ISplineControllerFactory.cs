using System;

namespace KochanekBartelsSplines.ViewModels.Interfaces
{
    public interface ISplineControllerFactory
    {
        ISplineController Get(Action redrawCallback);
    }
}
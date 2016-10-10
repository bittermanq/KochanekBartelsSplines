using System;
using KochanekBartelsSplines.ViewModels.Interfaces;

namespace KochanekBartelsSplines.ViewModels
{
    public class SplineControllerFactory : ISplineControllerFactory
    {
        public ISplineController Get(Action redrawCallback)
        {
            return new SplineController(redrawCallback);
        }
    }
}
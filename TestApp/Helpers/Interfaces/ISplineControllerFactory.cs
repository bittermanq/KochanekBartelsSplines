using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers.Interfaces
{
    public interface ISplineControllerFactory
    {
        ISplineSettingsController Get(RedrawCallback redrawCallback);
    }
}
namespace KochanekBartelsSplines.TestApp.Helpers.Interfaces
{
    public interface ISplineSettingsController
    {
        double Tension { get; set; }
        double Continuity { get; set; }
        double Bias { get; set; }
        int Segments { get; set; }
    }
}
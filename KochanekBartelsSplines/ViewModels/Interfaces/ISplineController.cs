namespace KochanekBartelsSplines.ViewModels.Interfaces
{
    public interface ISplineController
    {
        double Tension { get; set; }
        double Continuity { get; set; }
        double Bias { get; set; }
        int Segments { get; set; }
    }
}
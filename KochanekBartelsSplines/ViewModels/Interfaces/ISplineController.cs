namespace KochanekBartelsSplines.ViewModels.Interfaces
{
    internal interface ISplineController
    {
        double Tension { get; set; }
        double Continuity { get; set; }
        double Bias { get; set; }
        int Segments { get; set; }
    }
}
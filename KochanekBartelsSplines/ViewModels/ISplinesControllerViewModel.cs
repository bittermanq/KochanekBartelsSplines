using System.Drawing;
using GalaSoft.MvvmLight.Command;
using KochanekBartelsSplines.Helpers.Interfaces;

namespace KochanekBartelsSplines.ViewModels
{
    public interface ISplinesControllerViewModel
    {
        ISplinesController SplinesController { get; set; }

        RelayCommand<Point> MouseDownCommand { get; }
        RelayCommand<Point> MouseMoveCommand { get; }
        RelayCommand<Point> MouseDoubleClickCommand { get; }
        RelayCommand KeyDownCommand { get; }
        RelayCommand ResetCommand { get; }
        RelayCommand ClearCommand { get; }

        RelayCommand AddCurveCommand { get; }
        RelayCommand DeleteCurveCommand { get; }
        RelayCommand<int> SelectCurveCommand { get; }
    }
}
using System.Drawing;
using GalaSoft.MvvmLight.Command;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;

namespace KochanekBartelsSplines.TestApp.ViewModels.Interfaces
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
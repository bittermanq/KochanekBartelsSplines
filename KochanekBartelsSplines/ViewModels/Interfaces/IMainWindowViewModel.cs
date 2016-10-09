using System.Drawing;
using GalaSoft.MvvmLight.Command;
using KochanekBartelsSplines.Models;

namespace KochanekBartelsSplines.ViewModels.Interfaces
{
    internal interface IMainWindowViewModel
    {
        BitmapChannel BitmapChannel { get; set; }

        double Tension { get; set; }
        double Continuity { get; set; }
        double Bias { get; set; }
        int Segments { get; set; }

        RelayCommand<Point> MouseDownCommand { get;  }
        RelayCommand<Point> MouseMoveCommand { get;  }
        RelayCommand<Point> MouseDoubleClickCommand { get;  }
        RelayCommand KeyDownCommand { get;  }
        RelayCommand ResetCommand { get;  }
        RelayCommand ClearCommand { get;  }
    }
}
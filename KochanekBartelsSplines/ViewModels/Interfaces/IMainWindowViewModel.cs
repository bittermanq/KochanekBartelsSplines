using System.Drawing;
using GalaSoft.MvvmLight.Command;

namespace KochanekBartelsSplines.ViewModels.Interfaces
{
    internal interface IMainWindowViewModel
    {
        RelayCommand<Point> MouseDownCommand { get;  }
        RelayCommand<Point> MouseMoveCommand { get;  }
        RelayCommand<Point> MouseDoubleClickCommand { get;  }
        RelayCommand KeyDownCommand { get;  }
        RelayCommand ResetCommand { get;  }
        RelayCommand ClearCommand { get;  }
    }
}
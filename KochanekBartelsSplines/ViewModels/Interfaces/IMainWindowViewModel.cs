using GalaSoft.MvvmLight.Command;

namespace KochanekBartelsSplines.ViewModels.Interfaces
{
    internal interface IMainWindowViewModel
    {
        RelayCommand OpenFileCommand { get; }
        RelayCommand SaveFileCommand { get; }
        RelayCommand SaveNewFileCommand { get; }
    }
}
using GalaSoft.MvvmLight.Command;

namespace KochanekBartelsSplines.TestApp.ViewModels.Interfaces
{
    internal interface IMainWindowViewModel
    {
        RelayCommand OpenFileCommand { get; }
        RelayCommand SaveFileCommand { get; }
        RelayCommand SaveNewFileCommand { get; }
    }
}
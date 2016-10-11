using System.Windows;
using KochanekBartelsSplines.TestApp.ViewModels.Interfaces;

namespace KochanekBartelsSplines.TestApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        public MainWindow(IMainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;
        }
    }
}

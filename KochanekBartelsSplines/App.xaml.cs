using System.Windows;
using KochanekBartelsSplines.Unity;
using KochanekBartelsSplines.Views;

namespace KochanekBartelsSplines
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var unityConfigurator = new UnityConfigurator();
            var container = unityConfigurator.GetConfiguredContainer();

            var mainWindow = (MainWindow) container.Resolve(typeof(MainWindow), "MainWindow");
            mainWindow.Show();
        }
    }
}

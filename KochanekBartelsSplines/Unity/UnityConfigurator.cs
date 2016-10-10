using KochanekBartelsSplines.Helpers;
using KochanekBartelsSplines.Helpers.Interfaces;
using KochanekBartelsSplines.ViewModels;
using KochanekBartelsSplines.ViewModels.Interfaces;
using KochanekBartelsSplines.Views;
using Microsoft.Practices.Unity;

namespace KochanekBartelsSplines.Unity
{
    internal class UnityConfigurator
    {
        public UnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        private void RegisterTypes(UnityContainer container)
        {
            container.RegisterType<MainWindow, MainWindow>();

            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
            container.RegisterType<IInterpolatedPointsCalculator, InterpolatedPointsCalculator>();
            container.RegisterType<ILineInterpolator, LineInterpolator>();
            container.RegisterType<ISplineController, SplineController>();
            container.RegisterType<ISplineControllerFactory, SplineControllerFactory>();
        }
    }
}

using Microsoft.Practices.Unity;

namespace KochanekBartelsSplines.TestApp.Unity
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
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default);
        }
    }
}

using BaDemo.Main.View;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace BaDemo.Main
{
    public class MainModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public MainModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<object, MainView>("MainView");
            _container.RegisterType<object, HomeView>("HomeView");

            _regionManager.RegisterViewWithRegion("MainRegion", () => _container.Resolve<MainView>());
            _regionManager.RegisterViewWithRegion("ContentRegion", () => _container.Resolve<HomeView>());
        }
    }
}

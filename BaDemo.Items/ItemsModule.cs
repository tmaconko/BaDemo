using BaDemo.Items.Models.Repositories;
using BaDemo.Items.Views;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace BaDemo.Items
{
    public class ItemsModule : IModule
    {
        private readonly ILoggerFacade _logger;
        private readonly IUnityContainer _container;

        public ItemsModule(ILoggerFacade logger, IUnityContainer container)
        {
            _logger = logger;
            _container = container;
        }

        public void Initialize()
        {
            _logger.Log(string.Format("Started to load module {0}.", "ItemsModule"), Category.Info, Priority.Medium);

            _container.RegisterType<IItemsRepository, ItemsRepository>(new ContainerControlledLifetimeManager());

            _container.RegisterType<object, ItemListView>("ItemListView");
            _container.RegisterType<object, ItemCreateView>("ItemCreateView");
            _container.RegisterType<object, ItemUpdateView>("ItemUpdateView");
            _container.RegisterType<object, ItemDeleteView>("ItemDeleteView");

            _logger.Log(string.Format("Module {0} loaded successfully.", "ItemsModule"), Category.Info, Priority.Medium);
        }
    }
}

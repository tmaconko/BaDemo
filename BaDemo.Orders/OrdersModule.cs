using BaDemo.Orders.Models.Repositories;
using BaDemo.Orders.Models.Services;
using BaDemo.Orders.Views;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace BaDemo.Orders
{
    public class OrdersModule : IModule
    {
        private readonly ILoggerFacade _logger;
        private readonly IUnityContainer _container;

        public OrdersModule(ILoggerFacade logger, IUnityContainer container)
        {
            _logger = logger;
            _container = container;
        }

        public void Initialize()
        {
            _logger.Log(string.Format("Started to load module {0}.", "OrdersModule"), Category.Info, Priority.Medium);

            _container.RegisterType<IOrdersRepository, OrdersRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IEmailService, EmailService>(new ContainerControlledLifetimeManager());

            _container.RegisterType<object, OrderListView>("OrderListView");
            _container.RegisterType<object, OrderCreateView>("OrderCreateView");
            _container.RegisterType<object, OrderCreateAddItemsView>("OrderCreateAddItemsView");
            _container.RegisterType<object, OrderDetailsView>("OrderDetailsView");

            _logger.Log(string.Format("Module {0} loaded successfully.", "OrdersModule"), Category.Info, Priority.Medium);
        }
    }
}

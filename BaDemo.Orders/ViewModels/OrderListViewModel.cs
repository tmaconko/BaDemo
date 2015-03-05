using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BaDemo.Common;
using BaDemo.Items.Models;
using BaDemo.Items.ViewModels;
using BaDemo.Orders.Models;
using BaDemo.Orders.Models.Repositories;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Orders.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class OrderListViewModel : BindableBase, INavigationAware
    {
        private readonly IOrdersRepository _ordersRepository;
        private IRegionNavigationService _navigationService;

        public OrderListViewModel(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;

            Orders = new NotifyTaskCompletion<ObservableCollection<OrderViewModel>>(Task.Run(async () =>
            {
                var orders = await _ordersRepository.ListAsync();

                return new ObservableCollection<OrderViewModel>(orders.Select(Map));
            }));
        }

        public NotifyTaskCompletion<ObservableCollection<OrderViewModel>> Orders { get; private set; }

        public ICommand BackCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("HomeView", UriKind.Relative)));
            }
        }

        public ICommand InitiateOrderCommand
        {
            get { return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("OrderCreateView", UriKind.Relative))); }
        }

        public ICommand OrderDetailsCommand
        {
            get
            {
                return new DelegateCommand<OrderViewModel>(order =>
                {
                    var parameters = new NavigationParameters {{"Order", order}};
                    _navigationService.RequestNavigate(new Uri("OrderDetailsView", UriKind.Relative), parameters);
                }); 
                
            }
        }

        private static OrderViewModel Map(Order arg)
        {
            return new OrderViewModel
            {
                Id = arg.Id,
                ItemOrders = new ObservableCollection<ItemOrderViewModel>(arg.ItemOrders.Select(Map)),
                TimeStamp = arg.Timestamp,
                OrderTitle = arg.Title,
                Email = arg.Email,
            };
        }

        private static ItemOrderViewModel Map(ItemOrder arg)
        {
            return new ItemOrderViewModel
            {
                Count = arg.Count,
                OrderedItem = Map(arg.Item)
            };
        }

        private static ItemViewModel Map(Item item)
        {
            return new ItemViewModel
            {
                Id = item.Id,
                Description = item.Description,
                Title = item.Title,
                Code = item.Code,
            };
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = navigationContext.NavigationService;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}

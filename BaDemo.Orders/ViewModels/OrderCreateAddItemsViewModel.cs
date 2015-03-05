using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BaDemo.Common;
using BaDemo.Items.Models;
using BaDemo.Items.Models.Repositories;
using BaDemo.Items.ViewModels;
using BaDemo.Orders.Models;
using BaDemo.Orders.Models.Repositories;
using BaDemo.Orders.Models.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Orders.ViewModels
{
    public class OrderCreateAddItemsViewModel : BindableBase, INavigationAware
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IItemsRepository _itemsRepository;
        private readonly IEmailService _emailService;
        private IRegionNavigationService _navigationService;

        public OrderCreateAddItemsViewModel(
            IOrdersRepository ordersRepository, 
            IItemsRepository itemsRepository, 
            IEmailService emailService)
        {
            if (ordersRepository == null)
                throw new ArgumentNullException("ordersRepository");
            if (itemsRepository == null)
                throw new ArgumentNullException("itemsRepository");
            if (emailService == null)
                throw new ArgumentNullException("emailService");

            _ordersRepository = ordersRepository;
            _itemsRepository = itemsRepository;
            _emailService = emailService;

            Items = new NotifyTaskCompletion<ObservableCollection<ItemViewModel>>(Task.Run(async () =>
            {
                var items = await _itemsRepository.ListAsync();

                return new ObservableCollection<ItemViewModel>(items.Select(Map));
            }));
        }

        public NotifyTaskCompletion<ObservableCollection<ItemViewModel>> Items { get; private set; }

        private OrderViewModel _order;

        public OrderViewModel Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }

        public ICommand AddToOrderCommand
        {
            get 
            {
                return new DelegateCommand<ItemViewModel>(item =>
                {
                    Order.ItemOrders.Add(new ItemOrderViewModel {OrderedItem = item});
                });
            }
        }

        public ICommand RemoveFromOrderCommand
        {
            get
            {
                return new DelegateCommand<ItemOrderViewModel>(item =>
                {
                    Order.ItemOrders.Remove(item);
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("OrderListView", UriKind.Relative))); 
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.Journal.GoBack());
            }
        }

        public ICommand NextCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if (!Order.IsValid())
                        return;

                    if (!Order.ItemOrders.Any())
                        return;

                    var order = Map(Order);
                    await _ordersRepository.AddAsync(order);
                    await _emailService.SendEmail(order);

                    _navigationService.RequestNavigate(new Uri("OrderListView", UriKind.Relative));
                });
            }
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

        private static Order Map(OrderViewModel order)
        {
            return new Order
            {
                Timestamp = order.TimeStamp,
                Id = order.Id,
                Title = order.OrderTitle,
                Email = order.Email,
                ItemOrders = order.ItemOrders.Select(Map)
            };
        }

        private static ItemOrder Map(ItemOrderViewModel item)
        {
            return new ItemOrder
            {
                Count = item.Count,
                Item = new Item
                {
                    Id = item.OrderedItem.Id,
                    Code = item.OrderedItem.Code,
                    Description = item.OrderedItem.Description,
                    Title = item.OrderedItem.Title,
                }
            };
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = navigationContext.NavigationService;

            var order = navigationContext.Parameters["Order"] as OrderViewModel;
            if (order != null)
                Order = order;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add("Order", Order);
        }
    }
}

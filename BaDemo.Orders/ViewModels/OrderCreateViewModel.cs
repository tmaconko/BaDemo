using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Orders.ViewModels
{
    public class OrderCreateViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _navigationService;

        public OrderCreateViewModel()
        {
            Order = new OrderViewModel();
        }

        private OrderViewModel _order;

        public OrderViewModel Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("OrderListView", UriKind.Relative))); 
            }
        }

        public ICommand NextCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!Order.IsValid())
                        return;
                    
                    _navigationService.RequestNavigate(new Uri("OrderCreateAddItemsView", UriKind.Relative));
                });
            }
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

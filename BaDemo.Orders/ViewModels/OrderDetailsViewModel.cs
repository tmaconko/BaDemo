using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Orders.ViewModels
{
    public class OrderDetailsViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _navigationService;

        public ICommand CancelCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("OrderListView", UriKind.Relative)));
            }
        }

        private OrderViewModel _order;

        public OrderViewModel Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = navigationContext.NavigationService;
            Order = (OrderViewModel)navigationContext.Parameters["Order"];
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

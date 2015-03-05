using BaDemo.Orders.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Orders.Views
{
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class OrderCreateAddItemsView
    {
        public OrderCreateAddItemsViewModel ViewModel
        {
            get { return (OrderCreateAddItemsViewModel)DataContext; }
            set { DataContext = value; }
        }

        public OrderCreateAddItemsView(OrderCreateAddItemsViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }
    }
}

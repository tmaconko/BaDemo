using BaDemo.Orders.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Orders.Views
{
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class OrderListView
    {
        public OrderListViewModel ViewModel
        {
            get { return (OrderListViewModel) DataContext; }
            set { DataContext = value; }
        }

        public OrderListView(OrderListViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }
    }
}

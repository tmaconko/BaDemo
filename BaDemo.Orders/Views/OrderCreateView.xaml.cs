using BaDemo.Orders.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Orders.Views
{
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class OrderCreateView
    {
        public OrderCreateViewModel ViewModel
        {
            get { return (OrderCreateViewModel) DataContext; }
            set { DataContext = value; }
        }

        public OrderCreateView(OrderCreateViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }
    }
}

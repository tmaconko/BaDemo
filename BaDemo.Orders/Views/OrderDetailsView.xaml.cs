using BaDemo.Orders.ViewModels;

namespace BaDemo.Orders.Views
{
    public partial class OrderDetailsView
    {
        public OrderDetailsView(OrderDetailsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

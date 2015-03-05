using BaDemo.Items.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Items.Views
{
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class ItemListView
    {
        public ItemListView(ItemListViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

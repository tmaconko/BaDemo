using BaDemo.Items.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Items.Views
{
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class ItemDeleteView
    {
        public ItemDeleteView(ItemDeleteViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

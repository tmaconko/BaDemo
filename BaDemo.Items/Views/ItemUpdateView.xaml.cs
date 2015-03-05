using BaDemo.Items.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Items.Views
{
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class ItemUpdateView
    {
        public ItemUpdateView(ItemUpdateViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

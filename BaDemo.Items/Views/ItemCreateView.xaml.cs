using BaDemo.Items.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Items.Views
{
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class ItemCreateView
    {
        public ItemCreateView(ItemCreateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

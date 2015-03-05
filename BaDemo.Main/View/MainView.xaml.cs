using System.Windows.Controls;
using BaDemo.Main.ViewModel;

namespace BaDemo.Main.View
{
    public partial class MainView : UserControl
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

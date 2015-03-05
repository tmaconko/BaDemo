using System;
using System.Windows.Input;
using BaDemo.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Main.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IModuleManager _moduleManager;

        public MainViewModel(IRegionManager regionManager, IModuleManager moduleManager)
        {
            _regionManager = regionManager;
            _moduleManager = moduleManager;
        }

        public ICommand HomeCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _regionManager.RequestNavigate("ContentRegion", new Uri("HomeView", UriKind.Relative));
                });
            }
        }

        public ICommand OrdersListCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _moduleManager.LoadModule("BaDemo.Orders");
                    _regionManager.RequestNavigate("ContentRegion", new Uri("OrderListView", UriKind.Relative));
                });
            }
        }
        
        public ICommand ItemsListCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _moduleManager.LoadModule("BaDemo.Items");
                    _regionManager.RequestNavigate("ContentRegion", new Uri("ItemListView", UriKind.Relative));
                });
            }
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BaDemo.Common;
using BaDemo.Items.Models;
using BaDemo.Items.Models.Repositories;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Items.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ItemListViewModel : BindableBase, INavigationAware
    {
        private readonly IItemsRepository _itemsRepository;
        private IRegionNavigationService _navigationService;

        public ItemListViewModel(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;

            Items = new NotifyTaskCompletion<ObservableCollection<ItemViewModel>>(Task.Run(async () =>
            {
                var items = await _itemsRepository.ListAsync();

                return new ObservableCollection<ItemViewModel>(items.Select(Map));
            }));
        }

        public NotifyTaskCompletion<ObservableCollection<ItemViewModel>> Items { get; private set; }

        public ICommand BackCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("HomeView", UriKind.Relative)));
            }
        }

        public ICommand CreateItemCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("ItemCreateView", UriKind.Relative)));
            }
        }

        public ICommand UpdateItemCommand
        {
            get
            {
                return new DelegateCommand<ItemViewModel>(item =>
                {
                    var parameters = new NavigationParameters {{"Item", item}};
                    _navigationService.RequestNavigate(new Uri("ItemUpdateView", UriKind.Relative), parameters);
                });
            }
        }

        public ICommand DeleteItemCommand
        {
            get
            {
                return new DelegateCommand<ItemViewModel>(item =>
                {
                    var parameters = new NavigationParameters {{"Item", item}};
                    _navigationService.RequestNavigate(new Uri("ItemDeleteView", UriKind.Relative), parameters);
                });
            }
        }

        private static ItemViewModel Map(Item item)
        {
            return new ItemViewModel
            {
                Id = item.Id,
                Description = item.Description,
                Title = item.Title,
                Code = item.Code,
            };
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = navigationContext.NavigationService;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}

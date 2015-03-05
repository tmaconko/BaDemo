using System;
using System.Windows.Input;
using BaDemo.Items.Models;
using BaDemo.Items.Models.Repositories;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Items.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ItemUpdateViewModel : BindableBase, INavigationAware
    {
        private readonly IItemsRepository _itemsRepository;
        private IRegionNavigationService _navigationService;

        public ItemUpdateViewModel(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        private ItemViewModel _item;

        public ItemViewModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new DelegateCommand(() => _navigationService.RequestNavigate(new Uri("ItemListView", UriKind.Relative)));
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return DelegateCommand.FromAsyncHandler(async () =>
                {
                    if (!Item.IsValid())
                        return;

                    await _itemsRepository.UpdateAsync(Item.Id, Map(Item));

                    _navigationService.RequestNavigate(new Uri("ItemListView", UriKind.Relative));
                });
            }
        }

        private static Item Map(ItemViewModel item)
        {
            return new Item
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Code = item.Code,
            };
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = navigationContext.NavigationService;

            Item = (ItemViewModel)navigationContext.Parameters["Item"];
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

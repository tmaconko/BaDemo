using System;
using System.Windows.Input;
using BaDemo.Items.Models.Repositories;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace BaDemo.Items.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ItemDeleteViewModel : BindableBase, INavigationAware
    {
        private readonly IItemsRepository _itemsRepository;
        private IRegionNavigationService _navigationService;

        public ItemDeleteViewModel(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        private ItemViewModel _item;

        public ItemViewModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }

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
                return new DelegateCommand(() =>
                {
                    ConfirmationRequest.Raise(new Confirmation { Content = "Are you sure you want to remove this item from list?", Title = Item.Code }, async c =>
                    {
                        if (!c.Confirmed)
                            return;

                        await _itemsRepository.DeleteAsync(Item.Id);

                        _navigationService.RequestNavigate(new Uri("ItemListView", UriKind.Relative));  
                    });
                });
            }
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

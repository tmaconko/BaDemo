using System.ComponentModel.DataAnnotations;
using BaDemo.Common;
using BaDemo.Items.ViewModels;

namespace BaDemo.Orders.ViewModels
{
    public class ItemOrderViewModel : ValidationViewModel
    {
        private ItemViewModel _orderedItem;

        public ItemViewModel OrderedItem
        {
            get { return _orderedItem; }
            set { SetProperty(ref _orderedItem, value); }
        }

        private int _count;

        [Range(1, 10)]
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }
    }
}
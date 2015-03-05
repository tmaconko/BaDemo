using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using BaDemo.Common;

namespace BaDemo.Orders.ViewModels
{
    public class OrderViewModel : ValidationViewModel
    {
        public OrderViewModel()
        {
            ItemOrders = new ObservableCollection<ItemOrderViewModel>();
            TimeStamp = DateTime.Now;
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private DateTime _timeStamp;

        [Range(typeof(DateTime), "01.01.1900", "01.01.2099",
            ErrorMessage = "Specified date is not valid.")]
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { SetProperty(ref _timeStamp, value); }
        }

        private string _orderTitle;

        [Required(ErrorMessage = "Title cannot be empty.")]
        public string OrderTitle
        {
            get { return _orderTitle; }
            set { SetProperty(ref _orderTitle, value); }
        }

        private string _email;

        [Required(ErrorMessage = "Email cannot be empty.")]
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private ObservableCollection<ItemOrderViewModel> _itemOrders;
        public ObservableCollection<ItemOrderViewModel> ItemOrders
        {
            get { return _itemOrders; }
            set { SetProperty(ref _itemOrders, value); }
        }
    }
}

using System.ComponentModel.DataAnnotations;
using BaDemo.Common;

namespace BaDemo.Items.ViewModels
{
    public class ItemViewModel : ValidationViewModel
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _title;

        [Required(ErrorMessage = "Title cannot be empty.")]
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _code;

        [Required(ErrorMessage = "Code cannot be empty.")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Code must be 8 digit format.")]
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }
    }
}
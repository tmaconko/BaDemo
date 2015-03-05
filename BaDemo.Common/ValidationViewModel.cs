using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Mvvm;

namespace BaDemo.Common
{
    public class ValidationViewModel : BindableBase, IDataErrorInfo
    {
        protected IValidationHelper ValidationHelper { get; private set; }

        public ValidationViewModel()
        {
            ValidationHelper = new ValidationHelper(this);
        }

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            bool b = base.SetProperty(ref storage, value, propertyName);
            ValidationHelper.ValidateProperty(propertyName);

            return b;
        }
        
        public string this[string columnName]
        {
            get
            {
                if (ValidationHelper.Errors.ContainsKey(columnName))
                    return ValidationHelper.Errors[columnName];
                return string.Empty;
            }
        }

        string IDataErrorInfo.Error
        {
            get { throw new NotSupportedException(); }
        }

        public virtual bool IsValid()
        {
            Validate();
            return !ValidationHelper.HasErrors;
        }

        public virtual void Validate()
        {
            ValidationHelper.ValidateObject();
        }
    }
}
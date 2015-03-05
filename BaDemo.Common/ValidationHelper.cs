using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Mvvm;

namespace BaDemo.Common
{
    internal class ValidationHelper : IValidationHelper
    {
        private readonly BindableBase _viewModel;

        private readonly IDictionary<string, string> _errors;
        public IReadOnlyDictionary<string, string> Errors
        {
            get { return new ReadOnlyDictionary<string, string>(_errors); }
        }

        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }

        public ValidationHelper(BindableBase viewModel)
        {
            _viewModel = viewModel;

            _errors = new Dictionary<string, string>();
        }

        public void ValidateProperty([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                return;

            var propertyInfo = _viewModel.GetType().GetProperty(propertyName);

            try
            {
                Validator.ValidateProperty(propertyInfo.GetValue(_viewModel), new ValidationContext(_viewModel) { MemberName = propertyName });
                if (_errors.ContainsKey(propertyName))
                    _errors.Remove(propertyName);
            }
            catch (ValidationException e)
            {
                _errors[propertyName] = e.Message;
            }
        }

        public void ValidateObject()
        {
            foreach (var propertyInfo in _viewModel.GetType().GetProperties().Where(p => !p.GetIndexParameters().Any()))
            {
                try
                {
                    Validator.ValidateProperty(propertyInfo.GetValue(_viewModel),
                        new ValidationContext(_viewModel) {MemberName = propertyInfo.Name});
                    if (_errors.ContainsKey(propertyInfo.Name))
                        _errors.Remove(propertyInfo.Name);
                }
                catch (ValidationException e)
                {
                    _errors[propertyInfo.Name] = e.Message;
                }
            }
        }
    }
}

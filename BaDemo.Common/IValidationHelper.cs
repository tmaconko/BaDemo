using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BaDemo.Common
{
    public interface IValidationHelper
    {
        IReadOnlyDictionary<string, string> Errors { get; }
        bool HasErrors { get; }

        void ValidateProperty([CallerMemberName] string propertyName = null);
        void ValidateObject();
    }
}
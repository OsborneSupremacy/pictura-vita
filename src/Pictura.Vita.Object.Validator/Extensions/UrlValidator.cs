using FluentValidation;
using FluentValidation.Validators;

namespace Pictura.Vita.Object.Validator.Extensions
{
    public interface IUrlValidator { }

    public class UrlValidator<T> : PropertyValidator<T, string?>, IUrlValidator
    {
        public override string Name => "UrlValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) =>
            "'{PropertyName}' must not be a valid URL.";

        public override bool IsValid(ValidationContext<T> context, string? value)
        {
            if (value == null)
                return true;

            var options = new UriCreationOptions
            {
                DangerousDisablePathAndQueryCanonicalization = true
            };

            return Uri.TryCreate(value, options, out _);
        }
    }
}

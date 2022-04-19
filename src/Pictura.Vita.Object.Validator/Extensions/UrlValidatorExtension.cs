using FluentValidation;

namespace Pictura.Vita.Object.Validator.Extensions
{
    public static class UrlValidatorExtension
    {
        public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new UrlValidator<T>());
        }
    }
}
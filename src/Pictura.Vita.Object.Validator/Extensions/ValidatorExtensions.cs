using FluentValidation;

namespace Pictura.Vita.Object.Validator.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.SetValidator(new UrlValidator<T>());
}

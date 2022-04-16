using FluentValidation;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Object.Validator;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.NameParts.ToNullSafe().Count(x => !string.IsNullOrWhiteSpace(x)))
            .GreaterThan(0);
    }
}

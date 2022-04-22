using FluentValidation;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Object.Validator;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleForEach(x => x.NameParts).Length(0, 255);
        RuleForEach(x => x.TitleParts).Length(0, 255);
        RuleFor(x => x.NameParts.ToNullSafe().Count(x => !string.IsNullOrWhiteSpace(x)))
            .GreaterThan(0)
            .WithMessage("A name must be provided");
        RuleFor(x => x.Death)
            .GreaterThanOrEqualTo(x => x.Birth)
            .When(x => x.Birth.HasValue)
            .WithMessage($"{nameof(Person.Death)} cannot be earlier than {nameof(Person.Birth)}");
    }
}

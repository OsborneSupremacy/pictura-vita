using FluentValidation;

namespace Pictura.Vita.Object.Validator;

public class OrganizationValidator : AbstractValidator<Organization>
{
    public OrganizationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).Length(1, 255);
        RuleFor(x => x.End)
            .GreaterThanOrEqualTo(x => x.Start)
            .When(x => x.Start.HasValue)
            .WithMessage($"{nameof(Organization.End)} cannot be earlier than {nameof(Episode.Start)}");
    }
}

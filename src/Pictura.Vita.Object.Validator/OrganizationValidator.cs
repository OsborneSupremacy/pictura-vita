using FluentValidation;

namespace Pictura.Vita.Object.Validator;

public class OrganizationValidator : AbstractValidator<Organization>
{
    public OrganizationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

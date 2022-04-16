using FluentValidation;

namespace Pictura.Vita.Object.Validator;

public class TimelineValidator : AbstractValidator<Timeline>
{
    public TimelineValidator()
    {
        RuleFor(x => x.Title).Length(0, 255);
        RuleFor(x => x.Subtitle).Length(0, 255);
        RuleFor(x => x.Start).NotNull();
        RuleFor(x => x.Episodes).NotNull();
        RuleFor(x => x.Categories).NotNull();
    }
}

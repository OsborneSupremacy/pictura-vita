using FluentValidation;

namespace Pictura.Vita.Object.Validator;

public class TimelineValidator : AbstractValidator<Timeline>
{
    public TimelineValidator()
    {
        RuleFor(x => x.Title).Length(0, 255);
        RuleFor(x => x.Subtitle).Length(0, 255);
        RuleFor(x => x.Start).NotEmpty();
        RuleFor(x => x.End)
            .GreaterThanOrEqualTo(x => x.Start)
            .WithMessage($"{nameof(Timeline.End)} cannot be earlier than {nameof(Timeline.Start)}");
    }
}

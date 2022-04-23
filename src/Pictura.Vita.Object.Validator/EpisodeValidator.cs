using FluentValidation;
using Pictura.Vita.Object.Validator.Extensions;

namespace Pictura.Vita.Object.Validator;

public class EpisodeValidator : AbstractValidator<Episode>
{
    public EpisodeValidator()
    {
        RuleFor(x => x.EpisodeId).NotEmpty();
        RuleFor(x => x.Title).Length(1, 255);
        RuleFor(x => x.Subtitle).Length(0, 255);
        RuleFor(x => x.Description).Length(0, 5000);
        RuleFor(x => x.Url).Length(0, 2048);
        RuleFor(x => x.Url).Url();
        RuleFor(x => x.UrlDescription).Length(0, 255);
        RuleFor(x => x.Start).NotEmpty();
        RuleFor(x => x.End)
            .GreaterThanOrEqualTo(x => x.Start)
            .WithMessage($"{nameof(Episode.End)} cannot be earlier than {nameof(Episode.Start)}");
    }
}

using FluentValidation;

namespace Pictura.Vita.Object.Validator;

public class EpisodeValidator : AbstractValidator<Episode>
{
    public EpisodeValidator()
    {
        RuleFor(x => x.EpisodeId).NotEmpty();
        RuleFor(x => x.Title).Length(1, 255);
        RuleFor(x => x.Subtitle).Length(0, 255);
        RuleFor(x => x.Start).NotNull();
    }
}

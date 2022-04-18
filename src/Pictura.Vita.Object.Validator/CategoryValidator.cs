using FluentValidation;

namespace Pictura.Vita.Object.Validator;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Title).Length(1, 255);
        RuleFor(x => x.Subtitle).Length(0, 255);
        RuleFor(x => x.Privacy).NotNull();
        RuleFor(x => x.Privacy).NotEqual(Privacy.Inherit);
        RuleFor(x => x.EpisodeIds).NotNull();
        RuleForEach(x => x.EpisodeIds).NotEmpty();
    }
}

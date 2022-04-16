namespace Pictura.Vita.Object;

public record Timeline
{
    public DateOnly Start { get; set; }

    public DateOnly End { get; set; }

    public IList<Episode>? Episodes { get; set; }

    public IList<Category>? Categories { get; set; }
}

using Pictura.Vita.Interface;

namespace Pictura.Vita.Object;

public record Timeline : ITitled
{
    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public DateOnly Start { get; set; }

    /// <summary>
    /// If no end date, use the current date
    /// </summary>
    public DateOnly? End { get; set; }

    public IList<Episode>? Episodes { get; set; }

    public IList<Category>? Categories { get; set; }
}

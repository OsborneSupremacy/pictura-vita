using Pictura.Vita.Interface;

namespace Pictura.Vita.Object;

public record Category : ITitled
{
    public Guid CategoryId { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public Privacy Privacy { get; set; }

    public IList<Guid>? EpisodeIds { get; set; }
}

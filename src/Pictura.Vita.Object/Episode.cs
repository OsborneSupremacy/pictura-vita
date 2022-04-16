namespace Pictura.Vita.Object;

public record Episode
{
    public Guid EpisodeId { get; set; }

    public Privacy Privacy { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public string? UrlDescription { get; set; }

    public bool ObfuscateDates { get; set; }

    public EpisodeType EpisodeType { get; set; }

    public DateOnly Start { get; set; }

    public DateOnly? End { get; set; }

    public byte[]? Image { get; set; }
}

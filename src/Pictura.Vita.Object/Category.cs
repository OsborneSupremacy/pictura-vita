namespace Pictura.Vita.Object;

public record Category
{
    public Guid CategoryId { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public Privacy Privacy { get; set; }
}

namespace Pictura.Vita.Object;

public record Organization
{
    public string? Name { get; set; }

    public bool ObfuscateDates { get; set; }

    public DateOnly? Start { get; set; }

    public DateOnly? End { get; set; }
}

namespace Pictura.Vita.Object;

public record Organization
{
    public string? Name { get; set; }

    public bool ObfuscateDates { get; set; }

    public bool ApproximateStart { get; set; }

    public DatePrecision StartPrecision { get; set; }

    public DateOnly? Start { get; set; }

    public DatePrecision EndPrecision { get; set; }

    public DateOnly? End { get; set; }
}

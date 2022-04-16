namespace Pictura.Vita.Object;

public record class Person
{
    public IList<string>? NameParts { get; set; }

    public IList<string>? TitleParts { get; set; }

    public bool ObfuscateDates { get; set; }

    public DateOnly? Birth { get; set; }

    public DateOnly? Death { get; set; }
}

namespace Pictura.Vita.Object;

public record class TimelineSubject
{
    public SubjectType SubjectType { get; set; }

    public Organization? Organization { get; set; }

    public Person? Person { get; set; }

    public byte[]? Image { get; set; }
}

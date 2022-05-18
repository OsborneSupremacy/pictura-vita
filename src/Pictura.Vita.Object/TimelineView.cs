namespace Pictura.Vita.Object;

/// <summary>
/// A view of a <see cref="Timeline"/>, containing settings that can change which data is display
/// and which start/end date to use
/// </summary>
public record TimelineView
{
    public Timeline? Timeline { get; set; }

    public DateOnly Start { get; set; }

    public DateOnly End { get; set; }

    public bool ShowEmptyCategories { get; set; }
}

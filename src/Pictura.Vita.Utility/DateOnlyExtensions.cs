
namespace Pictura.Vita.Utility;

public static class DateOnlyExtensions
{
    public static int DayDiff(this DateOnly input, DateOnly comparisonDate) =>
        Math.Abs(input.DayNumber - comparisonDate.DayNumber);

    public static int DayDiff(this DateOnly? input, DateOnly comparisonDate)
    {
        if (!input.HasValue) return 1;
        return input.Value.DayDiff(comparisonDate);
    }

    public static int DayDiffFromLater(this DateOnly input, DateOnly comparisonDate) =>
        Functions.LaterOf(input, comparisonDate).DayDiff(comparisonDate);
}

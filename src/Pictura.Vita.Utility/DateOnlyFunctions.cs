
namespace Pictura.Vita.Utility;

public static partial class Functions
{
    public static DateOnly LaterOf(DateOnly date1, DateOnly date2) =>
        date1 > date2 ? date1 : date2;

    public static DateOnly LaterOf(params DateOnly[] input) => input.Max();

    public static DateOnly EarlierOf(DateOnly date1, DateOnly date2) =>
        date1 < date2 ? date1 : date2;

    public static DateOnly EarlierOf(params DateOnly[] input) => input.Min();

}

using System.Text;

namespace Pictura.Vita.Utility;

public static class StringExtensions
{
    public static DateOnly? ToDateOnlyOrDefault(this string? input, DateOnly? defaultVal) =>
        DateOnly.TryParse(input, out var outDate) ? outDate : defaultVal;

    public static DateOnly? ToDateOnlyOrDefault(this string? input) =>
        ToDateOnlyOrDefault(input, null);

    public static DateOnly? ToDateOnly(this string input) =>
        DateOnly.Parse(input);

    public static string Repeat(this string input, int count) =>
        new StringBuilder(input?.Length ?? 0 * count).Insert(0, input, count).ToString();

    public static string AppendIfNotWhitespace(this string? input, string? value, string separator)
    {
        if(string.IsNullOrWhiteSpace(value)) return input ?? string.Empty;
        return $"{input}{separator}{value}";
    }

    public static bool IsNullOrWhiteSpace(this string? input) =>
        string.IsNullOrWhiteSpace(input);

}

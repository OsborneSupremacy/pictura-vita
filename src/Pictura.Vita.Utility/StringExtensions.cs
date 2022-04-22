using System.Text;

namespace Pictura.Vita.Utility
{
    public static class StringExtensions
    {
        public static DateOnly? ToDateOnlyOrDefault(this string? input, DateOnly? defaultVal) =>
            DateOnly.TryParse(input, out var outDate) ? outDate : defaultVal;

        public static DateOnly? ToDateOnlyOrDefault(this string? input) =>
            ToDateOnlyOrDefault(input, null);

        public static DateOnly? ToDateOnly(this string input) =>
            DateOnly.Parse(input);

        public static string Repeat(this string input, int count) =>
            new StringBuilder(input.Length * count).Insert(0, input, count).ToString();
    }
}

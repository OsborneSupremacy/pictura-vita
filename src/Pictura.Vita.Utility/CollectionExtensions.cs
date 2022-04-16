namespace Pictura.Vita.Utility;

public static class CollectionExtensions
{
    public static IEnumerable<T> ToNullSafe<T>(this IEnumerable<T>? input) => 
        input ?? Enumerable.Empty<T>();

    public static IList<T> ToNullSafe<T>(this IList<T>? input) =>
        input ?? Enumerable.Empty<T>().ToList();
}

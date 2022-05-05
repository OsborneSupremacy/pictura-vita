namespace Pictura.Vita.Utility
{
    public static class GenericExtensions
    {
        public static T ToNullSafe<T>(this T? input) where T : new() =>
            input ?? new T();
    }
}

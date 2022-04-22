namespace Pictura.Vita.Utility
{
    public static class CharacterExtensions
    {
        public static string Repeat(this char input, int count) =>
            new(input, count);
    }
}

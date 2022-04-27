using System.Drawing;

namespace Pictura.Vita.Utility
{
    public static class ColorExtensions
    {
        public static string ToRgb(this Color input) =>
            $"rgb({input.R},{input.G},{input.B})";
    }
}

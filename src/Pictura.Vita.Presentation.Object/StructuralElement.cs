
namespace Pictura.Vita.Presentation.Object
{
    /// <summary>
    /// An <see cref="Element"/> that has dimensions necessary for presentation structure.
    /// </summary>
    public abstract record StructuralElement : Element
    {
        public int X { get; init; }

        public int Y { get; init; }

        public int YCenter => Y + (Height / 2);

        public int Height { get; init; }

        public int HalfWidth => Width / 2;

        public int Width { get; init; }
    }
}

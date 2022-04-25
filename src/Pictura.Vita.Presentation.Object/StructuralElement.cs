
namespace Pictura.Vita.Presentation.Object
{
    /// <summary>
    /// An <see cref="Element"/> that has dimensions necessary for presentation structure.
    /// </summary>
    public abstract record StructuralElement : Element
    {
        public int X { get; init; }

        public int Y { get; init; }

        public int Height { get; init; }

        public int Width { get; init; }
    }
}

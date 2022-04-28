
namespace Pictura.Vita.Presentation.Object;

/// <summary>
/// An <see cref="Element"/> that has dimensions necessary for presentation structure.
/// </summary>
public abstract record StructuralElement : Element
{
    public int X { get; set; }

    public int Y { get; set; }

    public int YCenter => Y + (Height / 2);

    public int Height { get; set; }

    public int HalfWidth => Width / 2;

    public int Width { get; set; }

    public int XEnd => X + Width;
}

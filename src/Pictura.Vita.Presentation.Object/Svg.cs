
namespace Pictura.Vita.Presentation.Object
{
    public record Svg : StructuralElement
    {
        public override string RenderOpen() =>
            $@"<svg viewBox=""{X} {Y} {Width} {Height}"" xmlns=""http://www.w3.org/2000/svg"">";

        public override string RenderClose() => "</svg>";
    }
}

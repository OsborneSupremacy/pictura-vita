
namespace Pictura.Vita.Presentation.Object
{
    public record SvgRect : StructuralElement
    {
        public override string RenderOpen() =>
            $@"<rect x=""{X}"" y=""{Y}"" width=""{Width}"" height=""{Height}"" style=""fill:rgb(0,0,255); stroke-width:3;stroke:rgb(0,0,0)"" />";

        public override string RenderClose() => String.Empty;
    }
}

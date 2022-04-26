
using System.Text;

namespace Pictura.Vita.Presentation.Object
{
    public record SvgRect : StructuralElement
    {
        public override string RenderOpen()
        {
            StringBuilder s = new();

            var rect = new TagBuilder("rect")
                .AddAttribute("x", X)
                .AddAttribute("y", Y)
                .AddAttribute("width", Width)
                .AddAttribute("height", Height)
                .AddAttribute("style", "fill:rgb(0,0,255); stroke-width:3; stroke:rgb(0,0,0)")
                .MakeSelfClosed();

            return rect.RenderOpen();
        }

        public override string RenderClose() => String.Empty;
    }
}

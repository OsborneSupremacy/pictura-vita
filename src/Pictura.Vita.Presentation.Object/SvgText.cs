
using System.Text;

namespace Pictura.Vita.Presentation.Object
{
    public record class SvgText : StructuralElement
    {
        public string? Content { get; init; }

        public override string RenderOpen()
        {
            StringBuilder s = new();

            // using a textPath is necessary to center/middle-align text with rectangle
            var pathId = Guid.NewGuid().ToString();

            var path = new SelfClosedRenderTag("path")
                .Add("id", pathId)
                .Add("d", $"M {X} {YCenter} L {X + Width} {YCenter}")
                .Add("stroke", "transparent");

            var text = new SeparateClosedRenderable("text");

            var textPath = new SeparateClosedRenderable("textPath")
                .Add("href", "#" + pathId)
                .Add("startoffset", X + Width / 2 - X)
                .Add("text-anchor", "middle")
                .Add("dominant-baseline", "middle")
                .Add("fill", "green")
                .Add("font-size", 100);

            s.Append(path.RenderOpen());
            s.Append(text.RenderOpen());
            s.Append(textPath.RenderOpen());

            if (!string.IsNullOrWhiteSpace(Content))
                s.Append(Content);

            s.Append(textPath.RenderClose());
            s.Append(text.RenderClose());

            return s.ToString();
        }

        public override string RenderClose() => "</textPath></text>";
    }
}

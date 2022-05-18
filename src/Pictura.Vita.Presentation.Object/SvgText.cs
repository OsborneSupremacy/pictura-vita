using Pictura.Vita.Utility;
using System.Drawing;
using System.Text;

namespace Pictura.Vita.Presentation.Object;

public record class SvgText : StructuralElement
{
    public string? Content { get; init; }

    public int FontSize = 100;

    public string DominantBaseLine = "middle";

    public override string RenderOpen()
    {
        StringBuilder s = new();

        // using a textPath is necessary to center/middle-align text with rectangle
        var pathId = Guid.NewGuid().ToString();

        var path = new TagBuilder("path")
            .AddAttribute("id", pathId)
            .AddAttribute(
                new AttributeBuilder("d",
                new ValueBuilder()
                    .UseDelimiter(' ')
                    .AddPart("M")
                    .AddPart(X)
                    .AddPart(YCenter)
                    .AddPart("L")
                    .AddPart(X + Width)
                    .AddPart(YCenter)
                )
            )
            // for debugging
            /*
            .AddAttribute(
                new AttributeBuilder("style", 
                new ValueBuilder()
                    .AddPart("stroke", Color.White.ToRgb())
                    .AddPart("stroke-width", 5)
                )
            )
            */
            .MakeSelfClosed();

        var text = new TagBuilder("text");

        var textPath = new TagBuilder("textPath")
            .AddAttribute("href", "#" + pathId)
            .AddAttribute("startoffset", X + Width / 2 - X)
            .AddAttribute("text-anchor", "middle")
            .AddAttribute("dominant-baseline", DominantBaseLine)
            .AddAttribute("fill", Color.White.ToRgb())
            .AddAttribute("font-size", FontSize);

        s.Append(path.RenderOpen());
        s.Append(text.RenderOpen());
        s.Append(textPath.RenderOpen());

        if (!string.IsNullOrWhiteSpace(Content))
            s.Append(Content);

        return s.ToString();
    }

    public override string RenderClose() => "</textPath></text>";
}

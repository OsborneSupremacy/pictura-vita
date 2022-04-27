
using Pictura.Vita.Utility;
using System.Drawing;

namespace Pictura.Vita.Presentation.Object
{
    public record SvgRect : StructuralElement
    {
        public Color? FillColor { get; init; }

        public override string RenderOpen() => 
            new TagBuilder("rect")
                .AddAttribute("x", X)
                .AddAttribute("y", Y)
                .AddAttribute("width", Width)
                .AddAttribute("height", Height)
                .AddAttribute(
                    new AttributeBuilder("style", 
                        new ValueBuilder()
                            .AddPart("fill", (FillColor ?? Color.Transparent).ToRgb())
                            .AddPart("stroke-width", 3)
                            .AddPart("stroke", Color.Black.ToRgb())
                        )
                )
                .MakeSelfClosed()
                .RenderOpen();

        public override string RenderClose() => string.Empty;
    }
}


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
                            .AddValuePart("fill", (FillColor ?? Color.Transparent).ToRgb())
                            .AddValuePart("stroke-width", "3")
                            .AddValuePart("stroke", "rgb(0,0,0)")
                        )
                )
                .MakeSelfClosed()
                .RenderOpen();

        public override string RenderClose() => string.Empty;
    }
}

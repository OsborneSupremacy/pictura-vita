
using Pictura.Vita.Utility;
using System.Drawing;

namespace Pictura.Vita.Presentation.Object
{
    public record Svg : StructuralElement
    {
        public override string RenderOpen() => 
            new TagBuilder("svg")
                .AddAttribute(
                    new AttributeBuilder("viewbox", 
                        new ValueBuilder()
                            .UseDelimiter(' ')
                            .AddPart(X)
                            .AddPart(Y)
                            .AddPart(Width)
                            .AddPart(Height)
                    )
                )
                .AddAttribute("xmlns", "http://www.w3.org/2000/svg")
                .AddAttribute(
                    new AttributeBuilder("style", 
                    new ValueBuilder()
                        .AddPart
                        (
                            new ValuePartBuilder("border")
                                .AddSubpart("1px")
                                .AddSubpart("solid")
                                .AddSubpart(Color.LightGreen.ToRgb())
                        )
                    )
                )
                .RenderOpen();

        public override string RenderClose() => "</svg>";
    }
}


using Pictura.Vita.Utility;
using System.Drawing;

namespace Pictura.Vita.Presentation.Object
{
    public record Svg : StructuralElement
    {
        public override string RenderOpen() => 
            new TagBuilder("svg")
                .AddAttribute("viewbox", $"{X} {Y} {Width} 10000")
                .AddAttribute("xmlns", "http://www.w3.org/2000/svg")
                .AddAttribute(
                    new AttributeBuilder("style", 
                        new ValueBuilder().AddValuePart("border", $"1px solid {Color.LightGreen.ToRgb()}")
                    )
                )
                .RenderOpen();

        public override string RenderClose() => "</svg>";
    }
}

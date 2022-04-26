
namespace Pictura.Vita.Presentation.Object
{
    public record SvgRect : StructuralElement
    {
        public override string RenderOpen() => 
            new TagBuilder("rect")
                .AddAttribute("x", X)
                .AddAttribute("y", Y)
                .AddAttribute("width", Width)
                .AddAttribute("height", Height)
                .AddAttribute(
                    new AttributeBuilder("style", 
                        new ValueBuilder()
                            .AddValuePart("fill", "rgb(0, 0, 255)")
                            .AddValuePart("stroke-width", "3")
                            .AddValuePart("stroke", "rgb(0,0,0)")
                        )
                )
                .MakeSelfClosed()
                .RenderOpen();

        public override string RenderClose() => string.Empty;
    }
}

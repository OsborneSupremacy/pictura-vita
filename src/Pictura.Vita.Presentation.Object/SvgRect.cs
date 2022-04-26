
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
                    new AttributeBuilder("style")
                    .AddValue("fill:rgb(0,0,255)")
                    .AddValue("stroke-width:3")
                    .AddValue("stroke:rgb(0,0,0)")
                )
                .MakeSelfClosed()
                .RenderOpen();

        public override string RenderClose() => String.Empty;
    }
}

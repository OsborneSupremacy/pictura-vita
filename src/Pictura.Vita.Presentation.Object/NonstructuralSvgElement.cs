using System.Text;

namespace Pictura.Vita.Presentation.Object
{
    public abstract record NonstructuralSvgElement : Element
    {
        public int X { get; init; }

        public int Y { get; init; }

        public abstract string? Tag { get; }

        public string? Content { get; init; }

        public override string RenderOpen()
        {
            StringBuilder s = new();
            if (!string.IsNullOrWhiteSpace(Tag))
                s.Append(
                    new TagBuilder(Tag)
                        .AddAttribute("x", X)
                        .AddAttribute("y", Y)
                        .RenderOpen()
                );
            if (!string.IsNullOrWhiteSpace(Content))
                s.Append(Content);
            return s.ToString();
        }

        public override string RenderClose()
        {
            StringBuilder s = new();
            if (!string.IsNullOrWhiteSpace(Tag))
                s.Append($"</{Tag}>");
            return s.ToString();
        }
    }
}

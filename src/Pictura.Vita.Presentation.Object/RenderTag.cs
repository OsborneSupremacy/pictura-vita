using System.Text;

namespace Pictura.Vita.Presentation.Object
{
    public static class RenderTagExtensions
    {
        public static RenderTag Add(this RenderTag input, string id, string value)
        {
            input.AddAttribute(id, value);
            return input;
        }
        public static RenderTag Add(this RenderTag input, string id, int value)
        {
            input.AddAttribute(id, value);
            return input;
        }
    }

    public class RenderTag
    {
        public RenderTag(string tag, bool isSelfClosed)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            IsSelfClosed = isSelfClosed;
            Attributes = new Dictionary<string, string>();
        }

        public string Tag { get; init; }

        public bool IsSelfClosed { get; init; }

        public Dictionary<string, string> Attributes { get; }

        public void AddAttribute(string id, string value) =>
            Attributes.Add(id, value);

        public void AddAttribute(string id, int value) =>
            Attributes.Add(id, value.ToString());

        public string RenderOpen()
        {
            StringBuilder s = new();
            s.Append($"<{Tag}");

            foreach (var (key, value) in Attributes)
                s.Append($@" {key}=""{value}""");

            if (IsSelfClosed)
                s.Append(" />");
            else
                s.Append('>');

            return s.ToString();
        }

        public string RenderClose() =>
            IsSelfClosed ? string.Empty : $"</{Tag}>";
    }

    public class SeparateClosedRenderable : RenderTag
    {
        public SeparateClosedRenderable(string tag) : base(tag, false)
        {
        }
    }

    public class SelfClosedRenderTag : RenderTag
    {
        public SelfClosedRenderTag(string tag) : base(tag, true)
        {
        }
    }
}

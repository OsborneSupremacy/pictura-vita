using System.Text;

namespace Pictura.Vita.Presentation.Object
{
    public static class TagBuilderExtensions
    {
        public static TagBuilder MakeSelfClosed(this TagBuilder input)
        {
            input.IsSelfClosed = true;
            return input;
        }

        public static TagBuilder AddAttribute(this TagBuilder input, string id, string value)
        {
            input.Attributes.Add(id, value);
            return input;
        }
        public static TagBuilder AddAttribute(this TagBuilder input, string id, int value)
        {
            input.AddAttribute(id, value.ToString());
            return input;
        }
    }

    public class TagBuilder
    {
        public TagBuilder(string tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            Attributes = new Dictionary<string, string>();
        }

        public string Tag { get; init; }

        public bool IsSelfClosed { get; set; }

        public Dictionary<string, string> Attributes { get; }

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

}

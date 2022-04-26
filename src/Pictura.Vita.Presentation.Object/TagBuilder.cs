﻿using System.Text;

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
            input.Attributes.Add(new AttributeBuilder(id).AddValue(value));
            return input;
        }
        public static TagBuilder AddAttribute(this TagBuilder input, string id, int value)
        {
            input.Attributes.Add(new AttributeBuilder(id).AddValue(value));
            return input;
        }
    }

    public class TagBuilder
    {
        public TagBuilder(string tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            Attributes = new List<AttributeBuilder>();
        }

        public string Tag { get; init; }

        public bool IsSelfClosed { get; set; }

        public List<AttributeBuilder> Attributes { get; }

        public string RenderOpen()
        {
            StringBuilder s = new();
            s.Append($"<{Tag}");

            foreach (var attribute in Attributes)
                s.Append(" " + attribute.Render());

            s.Append(IsSelfClosed ? " />" : '>');

            return s.ToString();
        }

        public string RenderClose() =>
            IsSelfClosed ? string.Empty : $"</{Tag}>";
    }
}

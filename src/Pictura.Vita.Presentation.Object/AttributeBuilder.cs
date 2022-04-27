
namespace Pictura.Vita.Presentation.Object
{
    public static class AttributeBuilderExtensions
    {
        public static AttributeBuilder AddValue(this AttributeBuilder input, string value)
        {
            input.ValueBuilder.AddPart(value);
            return input;
        }

        public static AttributeBuilder AddValue(this AttributeBuilder input, int value) =>
            AddValue(input, value.ToString());
    }

    public class AttributeBuilder
    {
        public AttributeBuilder(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ValueBuilder = new ValueBuilder();
        }

        public AttributeBuilder(string key, ValueBuilder valueBuilder)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ValueBuilder = valueBuilder;
        }

        public string Key { get; init; }

        public ValueBuilder ValueBuilder { get; set; }

        public string Render() =>
            $@"{Key}=""{ValueBuilder.Render()}""";
    }
}

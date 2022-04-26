
namespace Pictura.Vita.Presentation.Object
{
    public static class AttributeBuilderExtensions
    {
        public static AttributeBuilder AddValue(this AttributeBuilder input, string value)
        {
            input.Values.Add(value);
            return input;
        }
        public static AttributeBuilder AddValue(this AttributeBuilder input, int value)
        {
            input.Values.Add(value.ToString());
            return input;
        }
    }

    public class AttributeBuilder
    {
        public AttributeBuilder(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Values = new List<string>();
        }

        public string Key { get; init; }

        public List<string> Values { get; }

        public string Render() =>
            $@"{Key}=""{string.Join(';', Values)}""";
    }
}

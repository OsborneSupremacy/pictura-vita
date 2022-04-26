
namespace Pictura.Vita.Presentation.Object
{
    public static class ValueBuilderExtensions
    {
        public static ValueBuilder AddValuePart(this ValueBuilder input, string value) =>
            AddValuePart(input, string.Empty, value);

        public static ValueBuilder AddValuePart(this ValueBuilder input, int value) =>
            AddValuePart(input, string.Empty, value);

        public static ValueBuilder AddValuePart(this ValueBuilder input, string key, string value)
        {
            input.ValueParts.Add(new ValuePart()
            {
                Key = key,
                Value = value
            });
            return input;
        }

        public static ValueBuilder AddValuePart(this ValueBuilder input, string key, int value) =>
            AddValuePart(input, key, value.ToString());
    }

    public class ValueBuilder
    {
        public ValueBuilder()
        {
            ValueParts = new List<ValuePart>();
        }

        public ValueBuilder(ValuePart valuePart)
        {
            ValueParts = new List<ValuePart>
            {
                valuePart
            };
        }

        public List<ValuePart> ValueParts { get; set; }

        public string Render() =>
            string.Join(';', ValueParts.Select(x => x.Render()));
    }
}

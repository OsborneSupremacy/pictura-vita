
namespace Pictura.Vita.Presentation.Object
{
    public static class ValueBuilderExtensions
    {
        public static ValueBuilder AddValuePart(this ValueBuilder input, string value) =>
            AddValuePart(input, string.Empty, value);

        public static ValueBuilder AddValuePart(this ValueBuilder input, int value) =>
            AddValuePart(input, string.Empty, value);

        public static ValueBuilder AddValuePart(this ValueBuilder input, string key, int value) =>
            AddValuePart(input, key, value.ToString());

        public static ValueBuilder AddValuePart(this ValueBuilder input, ValuePartBuilder valuePartBuilder)
        {
            input.ValueParts.Add(valuePartBuilder);
            return input;
        }

        public static ValueBuilder AddValuePart(this ValueBuilder input, string key, string value)
        {
            input.ValueParts.Add(new ValuePartBuilder(key, value));
            return input;
        }
    }

    public class ValueBuilder
    {
        public ValueBuilder()
        {
            ValueParts = new List<ValuePartBuilder>();
        }

        public ValueBuilder(ValuePartBuilder valuePart)
        {
            ValueParts = new List<ValuePartBuilder>
            {
                valuePart
            };
        }

        public List<ValuePartBuilder> ValueParts { get; set; }

        public string Render() =>
            string.Join(';', ValueParts.Select(x => x.Render()));
    }
}

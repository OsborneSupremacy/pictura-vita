
namespace Pictura.Vita.Presentation.Object;

public static class ValueBuilderExtensions
{
    public static ValueBuilder AddPart(this ValueBuilder input, string value) =>
        AddPart(input, string.Empty, value);

    public static ValueBuilder AddPart(this ValueBuilder input, int value) =>
        AddPart(input, string.Empty, value);

    public static ValueBuilder AddPart(this ValueBuilder input, string key, int value) =>
        AddPart(input, key, value.ToString());

    public static ValueBuilder AddPart(this ValueBuilder input, ValuePartBuilder valuePartBuilder)
    {
        input.Parts.Add(valuePartBuilder);
        return input;
    }

    public static ValueBuilder AddPart(this ValueBuilder input, string key, string value)
    {
        input.Parts.Add(new ValuePartBuilder(key, value));
        return input;
    }

    public static ValueBuilder UseDelimiter(this ValueBuilder input, char delimiter)
    {
        input.Delimiter = delimiter;
        return input;
    }
}

public class ValueBuilder
{
    public ValueBuilder()
    {
        Parts = new List<ValuePartBuilder>();
        Delimiter = ';';
    }

    public char Delimiter { get; set; }

    public ValueBuilder(ValuePartBuilder valuePart)
    {
        Parts = new List<ValuePartBuilder>
        {
            valuePart
        };
        Delimiter = ';';
    }

    public List<ValuePartBuilder> Parts { get; set; }

    public string Render() =>
        string.Join(Delimiter, Parts.Select(x => x.Render()));
}


using System.Text;

namespace Pictura.Vita.Presentation.Object;

public static class ValuePartBuilderExtensions
{
    public static ValuePartBuilder AddSubpart(this ValuePartBuilder input, string value)
    {
        input.Subpart.Add(value);
        return input;
    }

    public static ValuePartBuilder AddSubpart(this ValuePartBuilder input, int value) =>
        AddSubpart(input, value.ToString());
}

public class ValuePartBuilder
{
    public ValuePartBuilder(string key)
    {
        Key = key;
        Subpart = new List<string>();
    }

    public ValuePartBuilder(string key, string value)
    {
        Key = key;
        Subpart = new List<string>
        {
            value
        };
    }

    public string? Key { get; }

    public List<string> Subpart { get; }

    public string Render()
    {
        StringBuilder s = new();
        if (!string.IsNullOrEmpty(Key))
            s.Append($"{Key}:");

        s.Append(string.Join(' ', Subpart));

        return s.ToString();
    }
}

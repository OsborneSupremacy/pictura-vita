
namespace Pictura.Vita.Presentation.Object
{
    public record ValuePart
    {
        public string? Key { get; init; }

        public string? Value { get; init; }

        public string Render()
        {
            if (string.IsNullOrWhiteSpace(Key)) return Value ?? string.Empty;
            return $"{Key}:{Value}";
        }
    }
}

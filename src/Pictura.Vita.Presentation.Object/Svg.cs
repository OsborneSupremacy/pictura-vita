
namespace Pictura.Vita.Presentation.Object
{
    public record Svg : StructuralElement
    {
        public override string RenderOpen() =>
            $@"<svg viewBox=""{X} {Y} {Width} 10000"" xmlns=""http://www.w3.org/2000/svg"" style=""border:1px solid rgb(20, 199, 29)"" >";

        public override string RenderClose() => "</svg>";
    }
}

﻿
using System.Text;

namespace Pictura.Vita.Presentation.Object
{
    public record class SvgText : StructuralElement
    {
        public string? Content { get; init; }

        public override string RenderOpen()
        {
            StringBuilder s = new();

            // using a textPath is necessary to center/middle-align text with rectangle
            var pathId = Guid.NewGuid().ToString();
            s.Append($@"<path id=""{pathId}"" d=""M 0 {YCenter} L {Width} {YCenter}"" stroke=""transparent"" />");
            s.Append($@"<text>");
            s.Append($@"<textPath href=""#{pathId}"" startoffset=""{Width / 2}"" text-anchor=""middle"" dominant-baseline=""middle"" fill=""green"" font-size=""100"" >");
            if (!string.IsNullOrWhiteSpace(Content))
                s.Append(Content);
            return s.ToString();
        }

        public override string RenderClose() => "</textPath></text>";
    }
}

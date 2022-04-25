using Pictura.Vita.Presentation.Object;
using System.Text;

namespace Pictura.Vita.Presentation.Service
{
    public class SvgWriter
    {
        public string Write(Element element)
        {
            StringBuilder s = new();
            s.Append(element.RenderOpen());
            foreach (var child in element.Children)
                s.Append(Write(child));
            s.Append(element.RenderClose());
            return s.ToString();
        }
    }
}

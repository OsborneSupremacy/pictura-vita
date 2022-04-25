using Pictura.Vita.Object;
using Pictura.Vita.Presentation.Object;

namespace Pictura.Vita.Presentation.Service
{
    public class SvgBuilder
    {
        public Svg Build(TimelineView view)
        {
            Svg svg = new()
            {
                X = 0,
                Y = 0,
                IsRoot = true,
                Height = 1000,
                Width = 1000
            };

            var runningY = 0;

            foreach(var cat in view.Timeline?.Categories ?? new List<Category>())
            {
                SvgRect rect = new()
                {
                    X = 100,
                    Y = runningY,
                    Width = 500,
                    Height = 100,
                    Order = svg.MaxChildOrder + 1
                };

                svg.AddChild(rect);

                SvgText text = new()
                {
                    X = 100,
                    Y = runningY,
                    Content = cat.Title,
                    Order = svg.MaxChildOrder + 1
                };

                svg.AddChild(text);

                runningY += 100;
            }

            return svg;
        }
    }
}
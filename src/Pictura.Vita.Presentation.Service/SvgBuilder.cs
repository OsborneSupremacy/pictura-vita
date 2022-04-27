using Pictura.Vita.Object;
using Pictura.Vita.Presentation.Object;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Presentation.Service
{
    public class SvgBuilder
    {
        public Svg Build(TimelineView view)
        {
            var totalDays = view.End.DayDiff(view.Start);

            Svg svg = new()
            {
                X = 0,
                Y = 0,
                IsRoot = true,
                Height = 10000,
                Width = totalDays
            };

            var runningY = 0;

            foreach(var cat in view.Timeline?.Categories ?? new List<Category>())
            {
                SvgRect rect = new()
                {
                    X = 0,
                    Y = runningY,
                    Width = totalDays,
                    Height = 500,
                    FillColor = System.Drawing.Color.Blue
                };

                svg.AddChild(rect);

                SvgText text = new()
                {
                    X = 0,
                    Y = runningY,
                    Width = totalDays,
                    Height = 500,
                    Content = cat.Title
                };

                svg.AddChild(text);

                var episodes = (view.Timeline?.Episodes ?? new List<Episode>())
                    .Where(x => cat.EpisodeIds.ToNullSafe().Contains(x.EpisodeId));

                foreach(var episode in episodes)
                {
                    runningY += 500;

                    SvgRect epRect = new()
                    {
                        X = episode.Start.DayDiff(view.Start),
                        Y = runningY,
                        Width = episode.End.DayDiff(episode.Start),
                        Height = 500,
                        FillColor = System.Drawing.Color.Red
                    };

                    svg.AddChild(epRect);

                    SvgText episodText = new()
                    {
                        X = episode.Start.DayDiff(view.Start),
                        Y = runningY,
                        Width = episode.End.DayDiff(episode.Start),
                        Height = 500,
                        Content = episode.Title
                    };

                    svg.AddChild(episodText);
                }

                runningY += 500;
            }

            return svg;
        }
    }
}
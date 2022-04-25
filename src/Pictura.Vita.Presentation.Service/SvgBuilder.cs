using Pictura.Vita.Object;
using Pictura.Vita.Presentation.Object;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Presentation.Service
{
    public class SvgBuilder
    {
        public Svg Build(TimelineView view)
        {
            var totalDays = view.End.DayNumber - view.Start.DayNumber;

            Svg svg = new()
            {
                X = 0,
                Y = 0,
                IsRoot = true,
                Height = 1000,
                Width = totalDays
            };

            var runningY = 0;

            foreach(var cat in view.Timeline?.Categories ?? new List<Category>())
            {
                SvgRect rect = new()
                {
                    X = 0, // for events, need to calculate based on start. but for categories, fine as is
                    Y = runningY,
                    Width = totalDays,
                    Height = 100
                };

                svg.AddChild(rect);

                SvgText text = new()
                {
                    X = 0,
                    Y = runningY,
                    Width = totalDays,
                    Height = 100,
                    Content = cat.Title
                };

                svg.AddChild(text);

                var episodes = (view.Timeline?.Episodes ?? new List<Episode>())
                    .Where(x => cat.EpisodeIds.ToNullSafe().Contains(x.EpisodeId));

                foreach(var episode in episodes)
                {
                    SvgText episodText = new()
                    {
                        X = 0,
                        Y = runningY,
                        Width = (episode.End?.DayNumber - episode.Start.DayNumber) ?? 1,
                        Height = 100,
                        Content = episode.Title
                    };

                    svg.AddChild(episodText);

                    runningY += 100;
                }

                runningY += 100;
            }

            return svg;
        }
    }
}
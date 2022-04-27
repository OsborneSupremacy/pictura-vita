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

            foreach (var cat in view.Timeline?.Categories ?? new List<Category>())
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

                foreach (var episode in episodes
                    .OrderBy(x => x.Start)
                    .ThenBy(x => x.End)
                    .ThenBy(x => x.Title))
                {
                    runningY += 500;

                    foreach (var element in BuildEpisodeElements(view, episode, runningY))
                        svg.AddChild(element);
                }

                runningY += 500;
            }

            return svg;
        }

        public List<Element> BuildEpisodeElements(
            TimelineView view,
            Episode episode,
            int runningY
            )
        {
            var effectiveStart = Functions.LaterOf(episode.Start, view.Start);
            var effectiveEnd = Functions.EarlierOf(episode.End ?? view.End, view.End);

            SvgRect rect = new()
            {
                X = effectiveStart.DayDiff(view.Start),
                Y = runningY,
                Width = episode.End.DayDiff(effectiveStart),
                Height = 500,
                FillColor = System.Drawing.Color.Red
            };

            SvgText text = new()
            {
                X = effectiveStart.DayDiff(view.Start),
                Y = runningY,
                Width = effectiveEnd.DayDiff(effectiveStart),
                Height = 500,
                Content = episode.Title.AppendIfNotWhitespace(episode.Subtitle, " - ")
            };

            return new List<Element> { rect, text };
        }
    }
}
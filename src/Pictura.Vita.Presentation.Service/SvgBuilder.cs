using Pictura.Vita.Object;
using Pictura.Vita.Presentation.Object;
using Pictura.Vita.Utility;
using System.Drawing;

namespace Pictura.Vita.Presentation.Service
{
    public class SvgBuilder
    {
        private readonly int _categoryHeight = 300;
        private readonly int _episodeHeight = 200;

        public Svg Build(TimelineView view)
        {
            var totalDays = view.End.DayDiff(view.Start);

            Svg svg = new()
            {
                X = 0,
                Y = 0,
                IsRoot = true,
                Width = totalDays
            };

            var runningHeight = 0;

            foreach (var cat in view.Timeline?.Categories ?? new List<Category>())
            {
                foreach (var element in BuildCategoryElements(cat, Color.Blue, totalDays, _categoryHeight, runningHeight))
                    svg.AddChild(element);

                runningHeight += _categoryHeight;

                var episodes = (view.Timeline?.Episodes ?? new List<Episode>())
                    .Where(x => cat.EpisodeIds.ToNullSafe().Contains(x.EpisodeId));

                foreach (var episode in episodes
                    .OrderBy(x => x.Start)
                    .ThenBy(x => x.End)
                    .ThenBy(x => x.Title))
                {
                    foreach (var element in BuildEpisodeElements(view, episode, Color.RebeccaPurple, _episodeHeight, runningHeight))
                        svg.AddChild(element);
                    runningHeight += _episodeHeight;
                }

            }

            svg.Height = runningHeight;
            return svg;
        }

        public List<Element> BuildCategoryElements(
            Category category,
            Color fillColor,
            int width,
            int height,
            int runningHeight
            )
        {
            SvgRect rect = new()
            {
                X = 0,
                Y = runningHeight,
                Width = width,
                Height = height,
                FillColor = fillColor
            };

            SvgText text = new()
            {
                X = 0,
                Y = runningHeight,
                Width = width,
                Height = height,
                Content = category.Title.AppendIfNotWhitespace(category.Subtitle, " - ")
            };

            return new(){ rect, text };
        }

        public List<Element> BuildEpisodeElements(
            TimelineView view,
            Episode episode,
            Color fillColor,
            int height,
            int runningHeight
            )
        {
            var effectiveStart = Functions.LaterOf(episode.Start, view.Start);
            var effectiveEnd = Functions.EarlierOf(episode.End ?? view.End, view.End);

            SvgRect rect = new()
            {
                X = effectiveStart.DayDiff(view.Start),
                Y = runningHeight,
                Width = episode.End.DayDiff(effectiveStart),
                Height = height,
                FillColor = fillColor
            };

            SvgText text = new()
            {
                X = effectiveStart.DayDiff(view.Start),
                Y = runningHeight,
                Width = effectiveEnd.DayDiff(effectiveStart),
                Height = height,
                Content = episode.Title.AppendIfNotWhitespace(episode.Subtitle, " - ")
            };

            return new(){ rect, text };
        }
    }
}
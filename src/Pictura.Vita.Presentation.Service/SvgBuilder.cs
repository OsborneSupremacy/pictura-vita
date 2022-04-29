using Pictura.Vita.Object;
using Pictura.Vita.Presentation.Object;
using Pictura.Vita.Utility;
using System.Drawing;

namespace Pictura.Vita.Presentation.Service;

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

        var runningY = 0;

        foreach (var cat in view.Timeline?.Categories ?? new List<Category>())
        {
            foreach (var element in BuildCategoryElements(cat, Color.Blue, totalDays, _categoryHeight, runningY))
                svg.AddChild(element);

            runningY += _categoryHeight;

            var episodes = (view.Timeline?.Episodes ?? new List<Episode>())
                .Where(x => cat.EpisodeIds.ToNullSafe().Contains(x.EpisodeId));

            List<SvgRect> categoryRectangles = new();

            bool episodesAdded = false;

            foreach (var episode in episodes
                .OrderBy(x => x.Start)
                .ThenBy(x => x.End)
                .ThenBy(x => x.Title))
            {
                episodesAdded = true;

                var (workingTier, workingY) = PlacementCalculator
                    .CalculateTier(runningY, _episodeHeight, categoryRectangles, episode.Start, view.Start);

                foreach (var element in BuildEpisodeElements(view, episode, Color.RebeccaPurple, workingTier, _episodeHeight, workingY))
                {
                    // adding to this list, because needed for CalculateTier
                    if (element is SvgRect svgRectElement)
                        categoryRectangles.Add(svgRectElement);
                    svg.AddChild(element);
                }

                // only increase runningY if workingY is greater
                if (workingY > runningY)
                    runningY = workingY;
            }

            if (episodesAdded)
                runningY += _episodeHeight;
        }

        svg.Height = runningY;
        return svg;
    }

    public List<Element> BuildCategoryElements(
        Category category,
        Color fillColor,
        int width,
        int height,
        int runningY
        ) => new()
        {
            new SvgRect
            {
                X = 0,
                Y = runningY,
                Width = width,
                Height = height,
                FillColor = fillColor
            },

            new SvgText
            {
                X = 0,
                Y = runningY,
                Width = width,
                Height = height,
                Content = category.Title.AppendIfNotWhitespace(category.Subtitle, " - ")
            }
        };

    public List<Element> BuildEpisodeElements(
        TimelineView view,
        Episode episode,
        Color fillColor,
        int tier,
        int height,
        int runningY
        )
    {
        var effectiveStart = Functions.LaterOf(episode.Start, view.Start);
        var effectiveEnd = Functions.EarlierOf(episode.End ?? view.End, view.End);

        return new()
        {
            new SvgRect
            {
                X = effectiveStart.DayDiff(view.Start),
                Y = runningY,
                Tier = tier,
                Width = episode.End.DayDiff(effectiveStart),
                Height = height,
                FillColor = fillColor
            },

            new SvgText()
            {
                X = effectiveStart.DayDiff(view.Start),
                Y = runningY,
                Width = effectiveEnd.DayDiff(effectiveStart),
                Height = height,
                Content = episode.Title.AppendIfNotWhitespace(episode.Subtitle, " - ")
            }

        };
    }
}

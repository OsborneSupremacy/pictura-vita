﻿using Pictura.Vita.Interface;
using Pictura.Vita.Object;
using Pictura.Vita.Presentation.Object;
using Pictura.Vita.Utility;
using System.Drawing;

namespace Pictura.Vita.Presentation.Service;

public class SvgBuilder
{
    private readonly int _categoryHeight = 300;
    private readonly int _episodeHeight = 200;
    private readonly int _titleHeight = 500;

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

        foreach (var element in BuildFullSpanElements(view.Timeline.ToNullSafe(), Color.Black, totalDays, _titleHeight, runningY))
            svg.AddChild(element);

        runningY += _titleHeight;

        foreach (var cat in view.Timeline.ToNullSafe().Categories.ToNullSafe())
            runningY = BuildCategory(view, svg, cat, totalDays, runningY);

        svg.Height = runningY;
        return svg;
    }

    public int BuildCategory(
        TimelineView view,
        Svg svg,
        Category category,
        int totalDays,
        int runningY
        )
    {
        foreach (var element in BuildFullSpanElements(category, Color.Blue, totalDays, _categoryHeight, runningY))
            svg.AddChild(element);

        runningY += _categoryHeight;

        var episodes = (view.Timeline.ToNullSafe().Episodes.ToNullSafe())
            .Where(x => category.EpisodeIds.ToNullSafe().Contains(x.EpisodeId));

        List<SvgRect> categoryRectangles = new();

        foreach (var episode in episodes
            .OrderBy(x => x.Start)
            .ThenBy(x => x.End)
            .ThenBy(x => x.Title))
            runningY = PlaceEpisode(view, svg, categoryRectangles, episode, runningY);

        // draw placeholder rectangle if no episodes in category
        if(!episodes.Any())
            svg.AddChild(
                new SvgRect
                {
                    X = 0,
                    Y = runningY,
                    Tier = 1,
                    Width = view.End.DayDiff(view.Start),
                    Height = _episodeHeight,
                    FillColor = Color.Transparent
                }
            );

        return runningY + _episodeHeight;
    }

    public int PlaceEpisode(
        TimelineView view,
        Svg svg,
        List<SvgRect> categoryRectangles,
        Episode episode,
        int runningY
        )
    {
        var (workingTier, workingY) = PlacementCalculator
            .CalculateTier(runningY, _episodeHeight, categoryRectangles, episode.Start, view.Start);

        AddEpisodeElements(
            svg,
            categoryRectangles,
            BuildEpisodeElements(view, episode, Color.RebeccaPurple, workingTier, _episodeHeight, workingY)
        );

        // only increase runningY if workingY is greater
        if (workingY > runningY)
            runningY = workingY;

        return runningY;
    }

    public List<Element> BuildFullSpanElements(
        ITitled titled,
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
                Content = titled.Title.AppendIfNotWhitespace(titled.Subtitle, " - ")
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
                X =  effectiveStart.DayDiff(view.Start),
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

    protected void AddEpisodeElements(
        Svg svg,
        List<SvgRect> categories,
        List<Element> elements)
    {
        foreach (var element in elements)
        {
            // adding to this list, because needed for CalculateTier
            if (element is SvgRect svgRectElement)
                categories.Add(svgRectElement);
            svg.AddChild(element);
        }
    }
}

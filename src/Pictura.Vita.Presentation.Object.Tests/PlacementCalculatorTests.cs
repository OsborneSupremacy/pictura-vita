using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Presentation.Object.Tests;

public class PlacementCalculatorTests
{
    [Fact]
    public void CalculateTier_1_When_First_Object()
    {
        // arrange
        var tierHeight = 100;

        List<SvgRect> rects = new();
        DateOnly episodeStart = new(2010, 2, 1);
        DateOnly viewStart = new(2000, 1, 1);

        // act
        var (tierOut, yOut) = PlacementCalculator
            .CalculateTier(tierHeight, tierHeight, rects, episodeStart, viewStart);

        // assert
        tierOut.Should().Be(1);
        yOut.Should().Be(tierHeight);
    }

    [Fact]
    public void CalculateTier_1_When_Episode_Does_Not_Overlap_Existing()
    {
        // arrange
        var tierHeight = 100;

        DateOnly episodeStart = new(2010, 2, 1);
        DateOnly viewStart = new(2000, 1, 1);

        List<SvgRect> rects = new();
        rects.Add(new SvgRect()
        {
            X = 0,
            Y = tierHeight,
            Tier = 1,
            Width = new DateOnly(2005, 1, 1).DayDiff(viewStart) // does not overlap
        });

        rects.Add(new SvgRect() // this rectangle on tier 2 should have no effect
        {
            X = 0,
            Y = tierHeight * 2,
            Tier = 2,
            Width = new DateOnly(2020, 2, 1).DayDiff(viewStart)
        });

        // act
        var (tierOut, yOut) = PlacementCalculator
            .CalculateTier(tierHeight, tierHeight, rects, episodeStart, viewStart);

        // assert
        tierOut.Should().Be(1);
        yOut.Should().Be(tierHeight);
    }

    [Fact]
    public void CalculateTier_2_When_Episode_Overlaps_Existing()
    {
        // arrange
        var tierHeight = 100;

        DateOnly episodeStart = new(2010, 2, 1);
        DateOnly viewStart = new(2000, 1, 1);

        List<SvgRect> rects = new();
        rects.Add(new SvgRect()
        {
            X = 0,
            Y = tierHeight,
            Tier = 1,
            Width = new DateOnly(2010, 2, 1).DayDiff(viewStart) // 1 day overlap
        });

        // act
        var (tierOut, yOut) = PlacementCalculator
            .CalculateTier(tierHeight, tierHeight, rects, episodeStart, viewStart);

        // assert
        tierOut.Should().Be(2);
        yOut.Should().Be(tierHeight * 2);
    }

}

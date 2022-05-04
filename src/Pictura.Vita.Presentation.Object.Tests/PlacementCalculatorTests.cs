using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace Pictura.Vita.Presentation.Object.Tests
{
    public class PlacementCalculatorTests
    {
        [Fact]
        public void CalculateTier_1_When_First_Object()
        {
            // arrange
            var firstTierY = 100;

            List<SvgRect> rects = new();
            DateOnly episodeStart = new(2010, 2, 1);
            DateOnly viewStart = new(2000, 1, 1);

            // act
            var (tierOut, yOut) = PlacementCalculator
                .CalculateTier(firstTierY, firstTierY, rects, episodeStart, viewStart);

            // assert
            tierOut.Should().Be(1);
            yOut.Should().Be(firstTierY);
        }

        [Fact]
        public void CalculateTier_1_When_Episode_Does_Not_Overlap_Existing()
        {
            var firstTierY = 100;

            List<SvgRect> rects = new();
            DateOnly episodeStart = new(2010, 2, 1);
            DateOnly viewStart = new(2000, 1, 1);


            // act
            var (tierOut, yOut) = PlacementCalculator
                .CalculateTier(firstTierY, firstTierY, rects, episodeStart, viewStart);

            // assert
            tierOut.Should().Be(1);
            yOut.Should().Be(firstTierY);
        }

        [Fact]
        public void CalculateTier_2_When_Episode_Overlaps_Existing()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void CalculateTier_3_When_Overlaps_All_Previous()
        {
            throw new System.NotImplementedException();
        }
    }
}

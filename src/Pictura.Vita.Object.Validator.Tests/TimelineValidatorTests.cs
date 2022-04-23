using FluentAssertions;
using FluentValidation;
using Pictura.Vita.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pictura.Vita.Object.Validator.Tests;

public class TimelineValidatorTests : AbstractValidator<Timeline>
{
    [Fact]
    public async Task Invalid_When_Title_Exceeds_Max()
    {
        // arrange
        Timeline timeline = new()
        {
            Title = '?'.Repeat(256),
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(2080, 12, 31)
        };

        TimelineValidator sut = new();

        // act
        var result = await sut.ValidateAsync(timeline);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Timeline.Title));
    }

    [Fact]
    public async Task Invalid_When_Start_Default()
    {
        // arrange
        Timeline timeline = new()
        {
            Title = '?'.Repeat(255),
            Start = default,
            End = new DateOnly(2080, 12, 31)
        };

        TimelineValidator sut = new();

        // act
        var result = await sut.ValidateAsync(timeline);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Timeline.Start));
    }

    [Fact]
    public async Task Invalid_When_End_Earlier_Than_Start()
    {
        // arrange
        Timeline timeline = new()
        {
            Title = '?'.Repeat(255),
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(1999, 12, 31)
        };

        TimelineValidator sut = new();

        // act
        var result = await sut.ValidateAsync(timeline);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Timeline.End));
    }

    [Theory]
    [InlineData("2000-01-01", "2080-12-31")]
    [InlineData("2000-01-01", default)]
    [InlineData("2000-01-01", "2000-01-01")]
    public async Task Valid(string start, string end)
    {
        // arrange
        Timeline timeline = new()
        {
            Title = '?'.Repeat(255),
            Subtitle = '?'.Repeat(255),
            Start = start.ToDateOnly()!.Value,
            End = end.ToDateOnlyOrDefault()
        };

        TimelineValidator sut = new();

        // act
        var result = await sut.ValidateAsync(timeline);

        // assert
        result.IsValid.Should().BeTrue();
    }
}

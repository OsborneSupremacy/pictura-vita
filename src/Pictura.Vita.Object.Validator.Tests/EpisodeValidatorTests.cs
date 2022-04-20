using Xunit;
using FluentAssertions;
using System.Linq;
using System;

namespace Pictura.Vita.Object.Validator.Tests;

public class EpisodeValidatorTests
{
    [Fact]
    public async void Invalid_When_EpisodeId_Default()
    {
        // arrange
        var category = new Episode
        {
            EpisodeId = new Guid(),
            Privacy = Privacy.Inherit,
            Title = "Title",
            Start = new DateOnly(2000, 1, 1)
        };

        var sut = new EpisodeValidator();

        // act
        var result = await sut.ValidateAsync(category);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain("Episode Id");
    }

    [Fact]
    public async void Invalid_When_Title_Exceeds_Max()
    {
        // arrange
        var episode = new Episode
        {
            EpisodeId = Guid.NewGuid(),
            Title = new string('?', 256),
            Privacy = Privacy.Inherit,
            Start = new DateOnly(2000, 1, 1)
        };

        var sut = new EpisodeValidator();

        // act
        var result = await sut.ValidateAsync(episode);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Episode.Title));
    }

    [Fact]
    public async void Invalid_When_Url_Invalid()
    {
        // arrange
        var episode = new Episode
        {
            EpisodeId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.Inherit,
            Url = "Bad URL",
            Start = new DateOnly(2000, 1, 1)
        };

        var sut = new EpisodeValidator();

        // act
        var result = await sut.ValidateAsync(episode);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Episode.Url));
    }

    [Fact]
    public async void Invalid_When_EndDate_LessThan_StartDate()
    {
        // arrange
        var episode = new Episode
        {
            EpisodeId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.Inherit,
            Url = "http://www.google.com",
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(1999, 12, 31)
        };

        var sut = new EpisodeValidator();

        // act
        var result = await sut.ValidateAsync(episode);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Episode.End));
    }

    [Theory]
    [InlineData("2000-01-01")]
    [InlineData("2000-01-02")]
    [InlineData("2000-01-10")]
    public async void Valid(string endDate)
    { 
        // arrange
        var episode = new Episode
        {
            EpisodeId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.Inherit,
            Url = "http://www.google.com",
            Start = new DateOnly(2000, 1, 1),
            End = DateOnly.Parse(endDate)
        };

        var sut = new EpisodeValidator();

        // act
        var result = await sut.ValidateAsync(episode);

        // assert
        result.IsValid.Should().BeTrue();
    }
}

using Xunit;
using FluentAssertions;
using System.Linq;
using System;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Object.Validator.Tests;

public class EpisodeValidatorTests
{
    [Fact]
    public async void Invalid_When_EpisodeId_Default()
    {
        // arrange
        Episode episode = new()
        {
            EpisodeId = new Guid(),
            Privacy = Privacy.Inherit,
            Title = "Title",
            Start = new DateOnly(2000, 1, 1)
        };

        EpisodeValidator sut = new();

        // act
        var result = await sut.ValidateAsync(episode);

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
        Episode episode = new()
        {
            EpisodeId = Guid.NewGuid(),
            Title = '?'.Repeat(256),
            Privacy = Privacy.Inherit,
            Start = new DateOnly(2000, 1, 1)
        };

        EpisodeValidator sut = new();

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
        Episode episode = new()
        {
            EpisodeId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.Inherit,
            Url = "Bad URL",
            Start = new DateOnly(2000, 1, 1)
        };

        EpisodeValidator sut = new();

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
        Episode episode = new()
        {
            EpisodeId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.Inherit,
            Url = "http://www.google.com",
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(1999, 12, 31)
        };

        EpisodeValidator sut = new();

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
        Episode episode = new()
        {
            EpisodeId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.Inherit,
            Url = "http://www.google.com",
            Start = new DateOnly(2000, 1, 1),
            End = endDate.ToDateOnly()
        };

        EpisodeValidator sut = new();

        // act
        var result = await sut.ValidateAsync(episode);

        // assert
        result.IsValid.Should().BeTrue();
    }
}

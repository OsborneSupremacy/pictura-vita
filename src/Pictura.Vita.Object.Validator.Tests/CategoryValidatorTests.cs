using Xunit;
using FluentAssertions;
using System.Linq;
using System;
using System.Collections.Generic;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Object.Validator.Tests;

public class CategoryValidatorTests
{
    [Fact]
    public async void Invalid_When_CategoryId_Default()
    {
        // arrange
        var category = new Category
        {
            CategoryId = new Guid(),
            Title = "Title",
            Privacy = Privacy.VisibleByDefault,
            EpisodeIds = Enumerable.Empty<Guid>().ToList()
        };

        var sut = new CategoryValidator();

        // act
        var result = await sut.ValidateAsync(category);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain("Category Id");
    }

    [Fact]
    public async void Invalid_When_Title_Exceeds_Max()
    {
        // arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Title = "?".Repeat(256),
            Privacy = Privacy.VisibleByDefault,
            EpisodeIds = Enumerable.Empty<Guid>().ToList()
        };

        var sut = new CategoryValidator();

        // act
        var result = await sut.ValidateAsync(category);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Category.Title));
    }

    /// <summary>
    /// Categories cannot have a <see cref="Privacy.Inherit"/>, because they
    /// the parent objects for episodes. They are not contained within objects
    /// that they can inherit from.
    /// </summary>
    [Fact]
    public async void Invalid_When_Privacy_Is_Inherit()
    {
        // arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.Inherit,
            EpisodeIds = Enumerable.Empty<Guid>().ToList()
        };

        var sut = new CategoryValidator();

        // act
        var result = await sut.ValidateAsync(category);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain(nameof(Category.Privacy));
    }

    [Fact]
    public async void Invalid_When_Episodes_Is_Null()
    {
        // arrange
        Category category = new()
        {
            CategoryId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.VisibleByDefault,
            EpisodeIds = null
        };

        CategoryValidator sut = new();

        // act
        var result = await sut.ValidateAsync(category);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain("Episode Ids");
    }

    [Fact]
    public async void Invalid_When_EpisodeId_Bad()
    {
        // arrange
        Category category = new()
        {
            CategoryId = Guid.NewGuid(),
            Title = "Title",
            Privacy = Privacy.VisibleByDefault,
            EpisodeIds = new List<Guid> { new Guid() }
        };

        CategoryValidator sut = new();

        // act
        var result = await sut.ValidateAsync(category);

        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors
            .First().ErrorMessage
            .Should()
            .Contain("Episode Ids");
    }

    [Fact]
    public async void Valid()
    {
        // arrange
        Category category = new()
        {
            CategoryId = Guid.NewGuid(),
            Title = "Title",
            Subtitle = "Subtitle",
            Privacy = Privacy.VisibleByDefault,
            EpisodeIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
        };

        CategoryValidator sut = new();

        // act
        var result = await sut.ValidateAsync(category);

        // assert
        result.IsValid.Should().BeTrue();
    }
}

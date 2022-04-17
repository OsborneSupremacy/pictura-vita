using Xunit;
using FluentAssertions;
using System.Linq;
using System;

namespace Pictura.Vita.Object.Validator.Tests;

public class CategoryValidatorTests
{
    [Fact]
    public async void Category_Invalid_When_CategoryId_Default()
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
    public async void Category_Invalid_When_Title_Exceeds_Max()
    {
        // arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Title = new string('?', 256),
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
}

using FluentAssertions;
using Pictura.Vita.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pictura.Vita.Object.Validator.Tests
{
    public class PersonValidatorTests
    {
        [Fact]
        public async Task Invalid_When_No_NameParts()
        {
            // arrange
            Person person = new()
            {
                NameParts = new List<string>() { string.Empty, string.Empty },
                Birth = new DateOnly(2000, 1, 1),
                Death = new DateOnly(2080, 12, 31)
            };

            PersonValidator sut = new();

            // act
            var result = await sut.ValidateAsync(person);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors
                .First().ErrorMessage
                .Should()
                .Contain("name");
        }

        [Fact]
        public async Task Invalid_When_One_NamePart_Exceeds_Max()
        {
            // arrange
            Person person = new()
            {
                NameParts = new List<string>() { "?".Repeat(256), string.Empty },
                Birth = new DateOnly(2000, 1, 1),
                Death = new DateOnly(2080, 12, 31)
            };

            PersonValidator sut = new();

            // act
            var result = await sut.ValidateAsync(person);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors
                .First().ErrorMessage
                .Should()
                .Contain("Name Parts");
        }

        [Fact]
        public async Task Invalid_When_Death_Before_Birth()
        {
            // arrange
            Person person = new()
            {
                NameParts = new List<string>() { "First", "Last" },
                Death = new DateOnly(2000, 1, 1),
                Birth = new DateOnly(2080, 12, 31)
            };

            PersonValidator sut = new();

            // act
            var result = await sut.ValidateAsync(person);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors
                .First().ErrorMessage
                .Should()
                .Contain(nameof(Person.Death));
        }

        [Theory]
        [InlineData(default, default)]
        [InlineData(default, "2080-12-31")]
        [InlineData("2000-01-01", "2080-12-31")]
        [InlineData("2000-01-01", default)]
        [InlineData("2000-01-01", "2000-01-01")]
        public async Task Valid(string birth, string death)
        {
            // arrange
            Person person = new()
            {
                NameParts = new List<string>() { "First", "Last" },
                Birth = birth.ToDateOnlyOrDefault(),
                Death = death.ToDateOnlyOrDefault()
            };

            PersonValidator sut = new();

            // act
            var result = await sut.ValidateAsync(person);

            // assert
            result.IsValid.Should().BeTrue();
        }
    }
}

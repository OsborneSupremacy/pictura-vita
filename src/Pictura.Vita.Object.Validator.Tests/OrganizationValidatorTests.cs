using Xunit;
using FluentAssertions;
using System.Linq;
using Pictura.Vita.Utility;

namespace Pictura.Vita.Object.Validator.Tests
{
    public class OrganizationValidatorTests
    {
        [Fact]
        public async void Invalid_When_Name_Exceeds_Max()
        {
            // arrange
            Organization organization = new()
            {
                Name = '?'.Repeat(256)
            };

            OrganizationValidator sut = new();

            // act
            var result = await sut.ValidateAsync(organization);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors
                .First().ErrorMessage
                .Should()
                .Contain(nameof(Organization.Name));
        }

        [Theory]
        [InlineData("2000-01-01", "1999-12-31")]
        public async void Invalid_Dates_Invalid(string start, string end)
        {
            // arrange
            Organization organization = new()
            {
                Name = '?'.Repeat(255),
                Start = start.ToDateOnly(),
                End = end.ToDateOnly()
            };

            OrganizationValidator sut = new();

            // act
            var result = await sut.ValidateAsync(organization);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors
                .First().ErrorMessage
                .Should()
                .Contain(nameof(Organization.End));
        }

        [Theory]
        [InlineData("2000-01-01", "2000-01-01")]
        [InlineData("2000-01-01", "2000-01-02")]
        [InlineData("2000-01-01", "")]
        [InlineData("", "1999-12-31")]
        public async void Valid(string start, string end)
        {
            // arrange
            Organization organization = new()
            {
                Name = '?'.Repeat(255),
                Start = start.ToDateOnlyOrDefault(),
                End = end.ToDateOnlyOrDefault()
            };

            OrganizationValidator sut = new();

            // act
            var result = await sut.ValidateAsync(organization);

            // assert
            result.IsValid.Should().BeTrue();
        }

    }
}

using Xunit;
using FluentAssertions;
using System.Linq;
using System;

namespace Pictura.Vita.Object.Validator.Tests
{
    public class OrganizationValidatorTests
    {
        [Fact]
        public async void Invalid_When_Name_Exceeds_Max()
        {
            // arrange
            var organization = new Organization
            {
                Name = new string('?', 256),
            };

            var sut = new OrganizationValidator();

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
            var organization = new Organization
            {
                Name = new string('?', 255),
                Start = DateOnly.Parse(start),
                End = DateOnly.Parse(end)
            };

            var sut = new OrganizationValidator();

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
            var organization = new Organization
            {
                Name = new string('?', 255),
                Start = DateOnly.TryParse(start, out var startDate) ? startDate : null,
                End = DateOnly.TryParse(end, out var endDate) ? endDate : null
            };

            var sut = new OrganizationValidator();

            // act
            var result = await sut.ValidateAsync(organization);

            // assert
            result.IsValid.Should().BeTrue();
        }

    }
}

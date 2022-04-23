using FluentAssertions;
using System;
using Xunit;

namespace Pictura.Vita.Utility.Tests;

public class CharacterExtensionsTests
{
    private readonly DateOnly _sampleDate1 = new (2000, 1, 1);

    [Fact]
    public void ToDateOnlyOrDefault_Valid_Date()
    {
        // arrange

        // act
        var result = "2000-01-01".ToDateOnlyOrDefault();

        // assert
        result.Should().Be(_sampleDate1);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a valid date")]
    public void ToDateOnlyOrDefault_Empty_Or_Invalid_String(string input)
    {
        // arrange

        // act
        var result = input.ToDateOnlyOrDefault();

        // assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a valid date")]
    public void ToDateOnlyOrDefault_Empty_Or_Invalid_String_Use_Default(string input)
    {
        // arrange

        // act
        var result = input.ToDateOnlyOrDefault(_sampleDate1);

        // assert
        result.Should().Be(_sampleDate1);
    }

    [Fact]
    public void ToDateOnly_Valid_Date()
    {
        // arrange

        // act
        var result = "2000-01-01".ToDateOnly();

        // assert
        result.Should().Be(_sampleDate1);
    }

    [Theory]
    [InlineData(default)]
    [InlineData("")]
    [InlineData("not a valid date")]
    public void ToDateOnly_Empty_Or_Invalid_String(string input)
    {
        // arrange

        // act
        Action result = () => {
            input.ToDateOnly();
        };

        // assert
        result.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData('c', 0, "")]
    [InlineData('c', 1, "c")]
    [InlineData('c', 10, "cccccccccc")]
    public void Repeat_Expected_Behavior(char input, int count, string expectedOut)
    {
        // arrange

        // act
        var result = input.Repeat(count);

        // assert
        result.Should().Be(expectedOut);
    }
}
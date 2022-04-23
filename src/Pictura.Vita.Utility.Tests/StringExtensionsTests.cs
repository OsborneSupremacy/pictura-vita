using FluentAssertions;
using System;
using Xunit;

namespace Pictura.Vita.Utility.Tests;

public class StringExtensionsTests
{
    private const string _multiLineTestIn = @"multi
line
test
";

    private const string _multiLineTestOut = @"multi
line
test
multi
line
test
";

    [Theory]
    [InlineData("s", 0, "")]
    [InlineData(default, 10, "")]
    [InlineData("s", 1, "s")]
    [InlineData("s", 10, "ssssssssss")]
    [InlineData("test ", 10, "test test test test test test test test test test ")]
    [InlineData("😀", 10, "😀😀😀😀😀😀😀😀😀😀")]
    [InlineData(_multiLineTestIn, 2, _multiLineTestOut)]
    public void Repeat_Expected_Behavior(string input, int count, string expectedOut)
    {
        // arrange

        // act
        var result = input.Repeat(count);

        // assert
        result.Should().Be(expectedOut);
    }

    private readonly DateOnly _sampleDate1 = new(2000, 1, 1);

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
}

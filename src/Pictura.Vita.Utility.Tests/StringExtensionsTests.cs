using FluentAssertions;
using Pictura.Vita.Utility;
using Xunit;

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
}

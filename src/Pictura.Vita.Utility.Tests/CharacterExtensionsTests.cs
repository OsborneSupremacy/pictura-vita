using FluentAssertions;
using System;
using Xunit;

namespace Pictura.Vita.Utility.Tests;

public class CharacterExtensionsTests
{
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
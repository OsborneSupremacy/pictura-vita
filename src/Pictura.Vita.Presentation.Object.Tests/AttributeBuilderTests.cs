using Xunit;
using FluentAssertions;

namespace Pictura.Vita.Presentation.Object.Tests;

public class AttributeBuilderTests
{
    [Fact]
    public void AddValue_Expected_Behavior()
    {
        // arrange
        var sut = new AttributeBuilder("height");

        // act
        sut.AddValue("100");
        var result = sut.Render();

        // asset
        result.Should().Be("height='100'");
    }
}

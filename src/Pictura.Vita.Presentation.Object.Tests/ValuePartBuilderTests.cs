using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace Pictura.Vita.Presentation.Object.Tests;

public class ValuePartBuilderTests
{
    [Fact]
    public void AddSubPart_Expected_Behavior()
    {
        // arrange
        var sut = new ValuePartBuilder("beer");

        // act
        sut.AddSubpart("double").AddSubpart("ipa");

        // assert
        sut.Render().Should().Be("beer:double ipa");
    }

}

using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace Pictura.Vita.Presentation.Object.Tests;

public class ValueBuilderTests
{
    [Fact]
    public void AddPart_Single_Expected_Behavior()
    {
        // arrange
        var sut = new ValueBuilder();

        // act
        sut.AddPart(1000);

        // assert
        sut.Render().Should().Be("1000");
    }

    [Fact]
    public void AddPart_Mutiple_Expected_Behavior()
    {
        // arrange
        var sut = new ValueBuilder();

        // act
        sut.AddPart("abv", 17).AddPart("ibu", 1000);

        // assert
        sut.Render().Should().Be("abv:17;ibu:1000");
    }
}

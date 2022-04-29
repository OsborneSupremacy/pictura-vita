using Xunit;
using FluentAssertions;

namespace Pictura.Vita.Presentation.Object.Tests
{
    public class TagBuilderTests
    {
        [Fact]
        public void AddAttribute_Expected_Behavior()
        {
            // arrange
            var sut = new TagBuilder("div");

            // act
            sut
                .AddAttribute("height", 100)
                .AddAttribute("width", 200)
                .MakeSelfClosed();

            var result = sut.RenderOpen();

            // assert
            result.Should().Be(@"<div height=""100"" width=""200"" />");
        }
    }
}

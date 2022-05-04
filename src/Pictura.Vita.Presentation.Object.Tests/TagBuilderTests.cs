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
                .AddAttribute("width", 200);

            // assert
            sut.RenderOpen().Should().Be(@"<div height=""100"" width=""200"">");
            sut.RenderClose().Should().Be(@"</div>");
        }

        [Fact]
        public void MakeSelfClosed_Expected_Behavior()
        {
            // arrange
            var sut = new TagBuilder("div");

            // act
            sut.MakeSelfClosed();

            // assert
            sut.RenderOpen().Should().Be(@"<div />");
        }
    }
}

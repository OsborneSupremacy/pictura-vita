using FluentAssertions;
using Pictura.Vita.Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace Pictura.Vita.Presentation.Service.Tests
{
    public class SvgWriterTests
    {
        [Fact]
        public void Write_Expected_Behavior()
        {
            // arrange
            TimelineView view = new()
            {
                Timeline = new Timeline()
                {
                    Title = "My Timeline",
                    Start = new DateOnly(1990, 1, 1),
                    End = new DateOnly(2020, 12, 31),
                    Categories = new List<Category>()
                    {
                        new Category() { Title = "Category 1" },
                        new Category() { Title = "Category 2" },
                        new Category() { Title = "Category 3" },
                        new Category() { Title = "Category 4" },
                        new Category() { Title = "Category 5" }
                    }
                }
            };

            var svg = new SvgBuilder().Build(view);

            var sut = new SvgWriter();

            // act
            var result = sut.Write(svg);

            // assert
            var tempFile = Path.ChangeExtension(Path.GetTempFileName(), "html");
            File.WriteAllText(tempFile, result);
            Process.Start("explorer.exe", tempFile);

            result.Should().NotBeNull();
        }
    }
}
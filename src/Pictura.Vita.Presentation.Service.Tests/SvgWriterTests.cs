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
            Dictionary<int, Guid> episodeDict = new() {
                { 1, Guid.NewGuid() },
                { 2, Guid.NewGuid() },
                { 3, Guid.NewGuid() },
                { 4, Guid.NewGuid() },
                { 5, Guid.NewGuid() },
            };

            TimelineView view = new()
            {
                Start = new DateOnly(1990, 1, 1),
                End = new DateOnly(2020, 12, 31),
                Timeline = new Timeline()
                {
                    Title = "My Timeline",

                    Start = new DateOnly(1990, 1, 1),
                    End = new DateOnly(2020, 12, 31),

                    Episodes = new List<Episode>
                    {
                        new Episode
                        {
                            Start = new DateOnly(1990, 1, 1),
                            End = new DateOnly(1999, 12, 31),
                            Title = "100 First Street",
                            EpisodeType = EpisodeType.Period,
                            EpisodeId = episodeDict[1]
                        },

                        new Episode
                        {
                            Start = new DateOnly(2000, 1, 1),
                            End = new DateOnly(2010, 12, 31),
                            Title = "200 Second Street",
                            EpisodeType = EpisodeType.Period,
                            EpisodeId = episodeDict[2]
                        },

                        new Episode
                        {
                            Start = new DateOnly(2011, 1, 1),
                            End = new DateOnly(2020, 12, 31),
                            Title = "300 Third Street",
                            EpisodeType = EpisodeType.Period,
                            EpisodeId = episodeDict[3]
                        },

                        new Episode
                        {
                            Start = new DateOnly(1995, 1, 1),
                            End = new DateOnly(2015, 12, 31),
                            Title = "1000 Beach Street",
                            Subtitle = "Vacation Home",
                            EpisodeType = EpisodeType.Period,
                            EpisodeId = episodeDict[4]
                        },

                        new Episode
                        {
                            Start = new DateOnly(1980, 1, 1),
                            End = new DateOnly(2000, 12, 31),
                            Title = "Nightmare House",
                            Subtitle = "Follows Me Wherever I Go",
                            EpisodeType = EpisodeType.Period,
                            EpisodeId = episodeDict[5]
                        }

                    },

                    Categories = new List<Category>
                    {
                        new Category()
                        { 
                            Title = "Residence",
                            EpisodeIds = new List<Guid>
                            {
                                episodeDict[1],
                                episodeDict[2],
                                episodeDict[3],
                                episodeDict[4],
                                episodeDict[5],
                            }
                        },
                        new Category { Title = "Category 2" },
                        new Category { Title = "Category 3" },
                        new Category { Title = "Category 4" },
                        new Category { Title = "Category 5" }
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
﻿using Pictura.Vita.Interface;

namespace Pictura.Vita.Object;

public record Episode : ITitled
{
    public Guid EpisodeId { get; set; }

    public Privacy Privacy { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public string? UrlDescription { get; set; }
    
    public EpisodeType EpisodeType { get; set; }

    public DatePrecision StartPrecision { get; set; }

    public DateOnly Start { get; set; }

    public DatePrecision EndPrecision { get; set; }

    public DateOnly? End { get; set; }
}

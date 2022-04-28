using Pictura.Vita.Utility;

namespace Pictura.Vita.Presentation.Object
{
    public static class PlacementCalculator
    {
        public static (int tierOut, int yOut) CalculateTier(
            int yIn,
            int heightIncrement,
            List<SvgRect> svgRects,
            DateOnly episodeStart,
            DateOnly viewStart
            )
        {
            // if this is first object, will go in first tier
            if (!svgRects.Any()) return (1, yIn);

            // get max tier
            var maxTier = svgRects.Max(x => x.Tier);

            // get starting X value of this episode
            var startX = Functions.LaterOf(episodeStart, viewStart).DayDiff(viewStart);

            // loop through tiers, starting with lowest
            for (int t = 1; t <= maxTier; t++)
            {
                var tierRects = svgRects.Where(x => x.Tier == t);

                // get rightmost border of rectangles on this tier
                var tierEnd = tierRects.Max(x => x.XEnd);

                // if rightmost order ends before startX, we can place this episode
                // on this tier
                if (tierEnd <= startX)
                    return (t, tierRects.First().Y);
            }

            // if we got this far, no existing tier has room for this episode
            return (maxTier + 1, yIn + heightIncrement);
        }
    }
}

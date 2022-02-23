using NodaTime;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime RoundNearest(this ZonedDateTime origin, Duration duration)
        {
            return origin.Zone.AtLeniently(origin.LocalDateTime.RoundNearest(duration));
        }
    }
}

using NodaTime;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime EndOf(this ZonedDateTime origin, SettleUnit unit)
        {
            return origin.Zone.AtLeniently(origin.LocalDateTime.EndOf(unit));
        }

        public static ZonedDateTime EndOf(this ZonedDateTime origin, IsoDayOfWeek weekday)
        {
            return origin.Zone.AtLeniently(origin.LocalDateTime.EndOf(weekday));
        }
    }
}

using NodaTime;
using System;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime StartOf(this ZonedDateTime origin, SettleUnit unit)
        {
            return origin.Zone.AtLeniently(origin.LocalDateTime.StartOf(unit));
        }

        public static ZonedDateTime StartOf(this ZonedDateTime origin, IsoDayOfWeek weekday)
        {
            return origin.Zone.AtLeniently(origin.LocalDateTime.StartOf(weekday));
        }
    }
}

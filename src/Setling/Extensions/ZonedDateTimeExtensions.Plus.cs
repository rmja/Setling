using NodaTime;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime PlusDaysLeniently(this ZonedDateTime origin, int days) =>
            origin.LocalDateTime.PlusDays(days).InZoneLeniently(origin.Zone);

        public static ZonedDateTime PlusMonthsLeniently(this ZonedDateTime origin, int months) =>
            origin.LocalDateTime.PlusMonths(months).InZoneLeniently(origin.Zone);

        public static ZonedDateTime PlusYearsLeniently(this ZonedDateTime origin, int years) =>
            origin.LocalDateTime.PlusYears(years).InZoneLeniently(origin.Zone);
    }
}

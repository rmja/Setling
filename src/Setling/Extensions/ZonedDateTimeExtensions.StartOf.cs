using NodaTime;
using System;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime StartOf(this ZonedDateTime origin, SettleUnit unit)
        {
            return unit switch
            {
                SettleUnit.Second => StartOfTicks(TimeSpan.TicksPerSecond),
                SettleUnit.Minute => StartOfTicks(TimeSpan.TicksPerMinute),
                SettleUnit.Hour => StartOfTicks(TimeSpan.TicksPerHour),
                SettleUnit.Day => StartOfTicks(TimeSpan.TicksPerDay),
                SettleUnit.Month => StartOfMonth(origin.Month),
                SettleUnit.Quarter => StartOfMonth(((origin.Month - 1) / 3) * 3 + 1),
                SettleUnit.Year => StartOfMonth(1),
                SettleUnit.Monday => origin.StartOf(IsoDayOfWeek.Monday),
                SettleUnit.Tuesday => origin.StartOf(IsoDayOfWeek.Tuesday),
                SettleUnit.Wednesday => origin.StartOf(IsoDayOfWeek.Wednesday),
                SettleUnit.Thursday => origin.StartOf(IsoDayOfWeek.Thursday),
                SettleUnit.Friday => origin.StartOf(IsoDayOfWeek.Friday),
                SettleUnit.Saturday => origin.StartOf(IsoDayOfWeek.Saturday),
                SettleUnit.Sunday => origin.StartOf(IsoDayOfWeek.Sunday),
                SettleUnit.January => StartOfMonth(1),
                SettleUnit.February => StartOfMonth(2),
                SettleUnit.March => StartOfMonth(3),
                SettleUnit.April => StartOfMonth(4),
                SettleUnit.May => StartOfMonth(5),
                SettleUnit.June => StartOfMonth(6),
                SettleUnit.July => StartOfMonth(7),
                SettleUnit.August => StartOfMonth(8),
                SettleUnit.September => StartOfMonth(9),
                SettleUnit.October => StartOfMonth(10),
                SettleUnit.November => StartOfMonth(11),
                SettleUnit.December => StartOfMonth(12),
                SettleUnit.Season => StartOfSeason(),
                SettleUnit.Spring => StartOfMonth(3),
                SettleUnit.Summer => StartOfMonth(6),
                SettleUnit.Autumn => StartOfMonth(9),
                SettleUnit.Winter => StartOfMonth(12),
                _ => throw new ArgumentException("Invalid start of unit", nameof(unit)),
            };

            ZonedDateTime StartOfTicks(long ticks)
            {
                var local = origin.LocalDateTime;
                var startOfTicks = local.PlusTicks(-(local.TickOfDay % ticks));
                return origin.Zone.AtLeniently(startOfTicks);
            }

            ZonedDateTime StartOfMonth(int month)
            {
                var local = origin.LocalDateTime;
                var startOfYear = local.Date.AtMidnight().PlusDays(-(local.DayOfYear - 1));
                var settled = startOfYear.PlusMonths(month - 1);
                return origin.Zone.AtLeniently(settled <= local ? settled : settled.PlusYears(-1));
            }

            ZonedDateTime StartOfSeason()
            {
                // 1 -> 12
                // 2 -> 12
                // 3 -> 3
                // 4 -> 3
                // 5 -> 3
                // 6 -> 6
                // 7 -> 6
                // 8 -> 6
                // 9 -> 9
                // 10 -> 9
                // 11 -> 9
                // 12 -> 12
                var season = (origin.Month % 12) / 3;
                return season > 0 ? StartOfMonth(season * 3) : StartOfMonth(12);
            }
        }

        public static ZonedDateTime StartOf(this ZonedDateTime origin, IsoDayOfWeek weekday)
        {
            var local = origin.LocalDateTime;
            var startOfMonday = local.Date.AtMidnight().PlusDays(-(int)local.DayOfWeek);
            var settled = startOfMonday.PlusDays((int)weekday);
            return origin.Zone.AtLeniently(settled <= local ? settled : settled.PlusDays(-7));
        }
    }
}

using NodaTime;
using System;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime StartOf(this ZonedDateTime origin, StartOfUnit unit)
        {
            return unit switch
            {
                StartOfUnit.Second => StartOfTicks(TimeSpan.TicksPerSecond),
                StartOfUnit.Minute => StartOfTicks(TimeSpan.TicksPerMinute),
                StartOfUnit.Hour => StartOfTicks(TimeSpan.TicksPerHour),
                StartOfUnit.Day => StartOfTicks(TimeSpan.TicksPerDay),
                StartOfUnit.Month => StartOfMonth(origin.Month),
                StartOfUnit.Quarter => StartOfMonth(((origin.Month - 1) / 3) * 3 + 1),
                StartOfUnit.Year => StartOfMonth(1),
                StartOfUnit.Monday => origin.StartOf(IsoDayOfWeek.Monday),
                StartOfUnit.Tuesday => origin.StartOf(IsoDayOfWeek.Tuesday),
                StartOfUnit.Wednesday => origin.StartOf(IsoDayOfWeek.Wednesday),
                StartOfUnit.Thursday => origin.StartOf(IsoDayOfWeek.Thursday),
                StartOfUnit.Friday => origin.StartOf(IsoDayOfWeek.Friday),
                StartOfUnit.Saturday => origin.StartOf(IsoDayOfWeek.Saturday),
                StartOfUnit.Sunday => origin.StartOf(IsoDayOfWeek.Sunday),
                StartOfUnit.January => StartOfMonth(1),
                StartOfUnit.February => StartOfMonth(2),
                StartOfUnit.March => StartOfMonth(3),
                StartOfUnit.April => StartOfMonth(4),
                StartOfUnit.May => StartOfMonth(5),
                StartOfUnit.June => StartOfMonth(6),
                StartOfUnit.July => StartOfMonth(7),
                StartOfUnit.August => StartOfMonth(8),
                StartOfUnit.September => StartOfMonth(9),
                StartOfUnit.October => StartOfMonth(10),
                StartOfUnit.November => StartOfMonth(11),
                StartOfUnit.December => StartOfMonth(12),
                StartOfUnit.Season => StartOfSeason(),
                StartOfUnit.Spring => StartOfMonth(3),
                StartOfUnit.Summer => StartOfMonth(6),
                StartOfUnit.Autumn => StartOfMonth(9),
                StartOfUnit.Winter => StartOfMonth(12),
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

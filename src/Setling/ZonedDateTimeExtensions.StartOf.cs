using NodaTime;
using System;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime StartOf(this ZonedDateTime origin, StartOfUnit unit)
        {
            switch (unit)
            {
                case StartOfUnit.Second:
                    return StartOfTicks(TimeSpan.TicksPerSecond);
                case StartOfUnit.Minute:
                    return StartOfTicks(TimeSpan.TicksPerMinute);
                case StartOfUnit.Hour:
                    return StartOfTicks(TimeSpan.TicksPerHour);
                case StartOfUnit.Day:
                    return StartOfTicks(TimeSpan.TicksPerDay);
                case StartOfUnit.Month:
                    return StartOfMonth(origin.Month);
                case StartOfUnit.Quarter:
                    return StartOfMonth(((origin.Month - 1) / 3) * 3 + 1);
                case StartOfUnit.Year:
                    return StartOfMonth(1);
                case StartOfUnit.Monday:
                    return StartOfWeekday(IsoDayOfWeek.Monday);
                case StartOfUnit.Tuesday:
                    return StartOfWeekday(IsoDayOfWeek.Tuesday);
                case StartOfUnit.Wednesday:
                    return StartOfWeekday(IsoDayOfWeek.Wednesday);
                case StartOfUnit.Thursday:
                    return StartOfWeekday(IsoDayOfWeek.Thursday);
                case StartOfUnit.Friday:
                    return StartOfWeekday(IsoDayOfWeek.Friday);
                case StartOfUnit.Saturday:
                    return StartOfWeekday(IsoDayOfWeek.Saturday);
                case StartOfUnit.Sunday:
                    return StartOfWeekday(IsoDayOfWeek.Sunday);
                case StartOfUnit.January:
                    return StartOfMonth(1);
                case StartOfUnit.February:
                    return StartOfMonth(2);
                case StartOfUnit.March:
                    return StartOfMonth(3);
                case StartOfUnit.April:
                    return StartOfMonth(4);
                case StartOfUnit.May:
                    return StartOfMonth(5);
                case StartOfUnit.June:
                    return StartOfMonth(6);
                case StartOfUnit.July:
                    return StartOfMonth(7);
                case StartOfUnit.August:
                    return StartOfMonth(8);
                case StartOfUnit.September:
                    return StartOfMonth(9);
                case StartOfUnit.October:
                    return StartOfMonth(10);
                case StartOfUnit.November:
                    return StartOfMonth(11);
                case StartOfUnit.December:
                    return StartOfMonth(12);
                case StartOfUnit.Season:
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
                case StartOfUnit.Spring:
                    return StartOfMonth(3);
                case StartOfUnit.Summer:
                    return StartOfMonth(6);
                case StartOfUnit.Autumn:
                    return StartOfMonth(9);
                case StartOfUnit.Winter:
                    return StartOfMonth(12);
                default:
                    throw new ArgumentException();
            }

            ZonedDateTime StartOfTicks(long ticks)
            {
                var local = origin.LocalDateTime;
                var startOfTicks = local.PlusTicks(-(local.TickOfDay % ticks));
                return origin.Zone.AtLeniently(startOfTicks);
            }

            ZonedDateTime StartOfWeekday(IsoDayOfWeek weekday)
            {
                var local = origin.LocalDateTime;
                var startOfMonday = local.Date.AtMidnight().PlusDays(-(int)local.DayOfWeek);
                var settled = startOfMonday.PlusDays((int)weekday);
                return origin.Zone.AtLeniently(settled <= local ? settled : settled.PlusDays(-7));
            }

            ZonedDateTime StartOfMonth(int month)
            {
                var local = origin.LocalDateTime;
                var startOfYear = local.Date.AtMidnight().PlusDays(-(local.DayOfYear - 1));
                var settled = startOfYear.PlusMonths(month - 1);
                return origin.Zone.AtLeniently(settled <= local ? settled : settled.PlusYears(-1));
            }
        }
    }
}

using NodaTime;
using System;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static LocalDateTime EndOf(this LocalDateTime origin, SettleUnit unit)
        {
            var startOf = origin.StartOf(unit);
            return unit switch
            {
                SettleUnit.Second => startOf.PlusSeconds(1),
                SettleUnit.Minute => startOf.PlusMinutes(1),
                SettleUnit.Hour => startOf.PlusHours(1),
                SettleUnit.Day => startOf.PlusDays(1),
                SettleUnit.Month => startOf.PlusMonths(1),
                SettleUnit.Quarter => startOf.PlusMonths(3),
                SettleUnit.Year => startOf.PlusYears(1),
                SettleUnit.Monday or
                SettleUnit.Tuesday or
                SettleUnit.Wednesday or
                SettleUnit.Thursday or
                SettleUnit.Friday or
                SettleUnit.Saturday or
                SettleUnit.Sunday => startOf.PlusDays(7),
                SettleUnit.January or
                SettleUnit.February or
                SettleUnit.March or
                SettleUnit.April or
                SettleUnit.May or
                SettleUnit.June or
                SettleUnit.July or
                SettleUnit.August or
                SettleUnit.September or
                SettleUnit.October or
                SettleUnit.November or
                SettleUnit.December => startOf.PlusYears(1),
                SettleUnit.Season => startOf.PlusMonths(3),
                SettleUnit.Spring or
                SettleUnit.Summer or
                SettleUnit.Autumn or
                SettleUnit.Winter => startOf.PlusYears(1),
                _ => throw new InvalidOperationException(),
            };
        }

        public static LocalDateTime EndOf(this LocalDateTime origin, IsoDayOfWeek weekday)
        {
            return origin.StartOf(weekday).PlusDays(7);
        }
    }
}

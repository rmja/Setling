using NodaTime;
using System;

namespace Setling
{
    public static partial class ZonedDateTimeExtensions
    {
        public static ZonedDateTime EndOf(this ZonedDateTime origin, SettleUnit unit)
        {
            var startOf = origin.StartOf(unit);
            return unit switch
            {
                SettleUnit.Second => startOf.PlusSeconds(1),
                SettleUnit.Minute => startOf.PlusMinutes(1),
                SettleUnit.Hour => startOf.PlusHours(1),
                SettleUnit.Day => startOf.PlusDaysLeniently(1),
                SettleUnit.Month => startOf.PlusMonthsLeniently(1),
                SettleUnit.Quarter => startOf.PlusMonthsLeniently(3),
                SettleUnit.Year => startOf.PlusYearsLeniently(1),
                SettleUnit.Monday or
                SettleUnit.Tuesday or
                SettleUnit.Wednesday or
                SettleUnit.Thursday or
                SettleUnit.Friday or
                SettleUnit.Saturday or
                SettleUnit.Sunday => startOf.PlusDaysLeniently(7),
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
                SettleUnit.December => startOf.PlusYearsLeniently(1),
                SettleUnit.Season => startOf.PlusMonthsLeniently(3),
                SettleUnit.Spring or
                SettleUnit.Summer or
                SettleUnit.Autumn or
                SettleUnit.Winter => startOf.PlusYearsLeniently(1),
                _ => throw new InvalidOperationException(),
            };
        }

        public static ZonedDateTime EndOf(this ZonedDateTime origin, IsoDayOfWeek weekday)
        {
            return origin.StartOf(weekday).PlusDaysLeniently(7);
        }
    }
}

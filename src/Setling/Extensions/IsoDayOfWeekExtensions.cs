using NodaTime;
using System;

namespace Setling
{
    public static class IsoDayOfWeekExtensions
    {
        public static StartOfUnit ToStartOfUnit(this IsoDayOfWeek dayOfWeek) => dayOfWeek switch
        {
            IsoDayOfWeek.Monday => StartOfUnit.Monday,
            IsoDayOfWeek.Tuesday => StartOfUnit.Tuesday,
            IsoDayOfWeek.Wednesday => StartOfUnit.Wednesday,
            IsoDayOfWeek.Thursday => StartOfUnit.Thursday,
            IsoDayOfWeek.Friday => StartOfUnit.Friday,
            IsoDayOfWeek.Saturday => StartOfUnit.Saturday,
            IsoDayOfWeek.Sunday => StartOfUnit.Sunday,
            _ => throw new ArgumentException("Invalid day of week", nameof(dayOfWeek)),
        };
    }
}

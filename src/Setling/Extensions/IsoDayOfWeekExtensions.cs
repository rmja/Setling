using NodaTime;
using System;

namespace Setling
{
    public static class IsoDayOfWeekExtensions
    {
        public static SettleUnit ToSettleUnit(this IsoDayOfWeek dayOfWeek) => dayOfWeek switch
        {
            IsoDayOfWeek.Monday => SettleUnit.Monday,
            IsoDayOfWeek.Tuesday => SettleUnit.Tuesday,
            IsoDayOfWeek.Wednesday => SettleUnit.Wednesday,
            IsoDayOfWeek.Thursday => SettleUnit.Thursday,
            IsoDayOfWeek.Friday => SettleUnit.Friday,
            IsoDayOfWeek.Saturday => SettleUnit.Saturday,
            IsoDayOfWeek.Sunday => SettleUnit.Sunday,
            _ => throw new ArgumentException("Invalid day of week", nameof(dayOfWeek)),
        };
    }
}

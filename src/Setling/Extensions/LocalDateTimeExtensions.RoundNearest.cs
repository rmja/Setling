using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Setling
{
    public static partial class LocalDateTimeExtensions
    {
        private static Dictionary<Duration, Func<LocalDateTime, LocalDateTime>> _nearestRounders = new();

        public static LocalDateTime RoundNearest(this LocalDateTime origin, Duration duration)
        {
            if (!_nearestRounders.TryGetValue(duration, out var rounder))
            {
                rounder = CreateNearestRounder(duration.ToTimeSpan());

                var clone = _nearestRounders.ToDictionary(x => x.Key, x => x.Value);
                clone[duration] = rounder;
                _nearestRounders = clone;
            }

            return rounder(origin);
        }

        private static Func<LocalDateTime, LocalDateTime> CreateNearestRounder(TimeSpan interval) => input =>
        {
            var delta = input.TickOfDay % interval.Ticks;
            var roundUp = delta > interval.Ticks / 2;
            if (roundUp)
            {
                var roundUpDelta = (interval.Ticks - (input.TickOfDay % interval.Ticks)) % interval.Ticks;
                return input.PlusTicks(roundUpDelta);
            }
            else
            {
                return input.PlusTicks(-delta);
            }
        };
    }
}

using NodaTime;
using System.Collections.Generic;
using Xunit;

namespace Setling.Tests.Extensions
{
    public class ZonedDateTimeExtensions_RoundNearestTests
    {
        private static readonly DateTimeZone Timezone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/Copenhagen");

        [Theory]
        [MemberData(nameof(GetData))]
        public void RoundNearest(LocalDateTime origin, Duration interval, LocalDateTime expected)
        {
            var zonedOrigin = origin.InZoneLeniently(Timezone);
            var rounded = zonedOrigin.RoundNearest(interval);

            Assert.Equal(expected.InZoneLeniently(Timezone), rounded);
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] { new LocalDateTime(2019, 5, 20, 0, 2, 25), Duration.FromMinutes(5), new LocalDateTime(2019, 5, 20, 00, 00, 00) };
            yield return new object[] { new LocalDateTime(2019, 5, 20, 0, 3, 25), Duration.FromMinutes(5), new LocalDateTime(2019, 5, 20, 00, 05, 00) };
            yield return new object[] { new LocalDateTime(2019, 5, 20, 5, 52, 25), Duration.FromMinutes(5), new LocalDateTime(2019, 5, 20, 05, 50, 00) };
            yield return new object[] { new LocalDateTime(2019, 5, 20, 5, 58, 25), Duration.FromMinutes(5), new LocalDateTime(2019, 5, 20, 06, 00, 00) };
            yield return new object[] { new LocalDateTime(2019, 5, 20, 23, 58, 25), Duration.FromMinutes(5), new LocalDateTime(2019, 5, 21, 00, 00, 00) };
        }
    }
}

using NodaTime;
using Setling.Parts;
using Xunit;

namespace Setling.Tests.Parts
{
    public class StartOfPartTests
    {
        [Fact]
        public void RoundsToStartOfDay()
        {
            // Given
            var origin = new LocalDateTime(2021, 11, 19, 14, 05, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);
            var expected = new LocalDateTime(2021, 11, 19, 00, 00, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);

            // When
            var rounded = new StartOfPart(SettleUnit.Day).Apply(origin);

            // Then
            Assert.Equal(expected, rounded);
        }
    }
}

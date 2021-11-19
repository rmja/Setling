using NodaTime;
using Setling.Parts;
using Xunit;

namespace Setling.Tests.Parts
{
    public class EndOfPartTests
    {
        [Fact]
        public void RoundsToEndOfDay()
        {
            // Given
            var origin = new LocalDateTime(2021, 11, 19, 11, 05, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);
            var expected = new LocalDateTime(2021, 11, 20, 00, 00, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);

            // When
            var rounded = new EndOfPart(SettleUnit.Day).Apply(origin);

            // Then
            Assert.Equal(expected, rounded);
        }
    }
}

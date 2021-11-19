using NodaTime;
using Setling.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Setling.Tests.Parts
{
    public class NearestPartTests
    {
        [Fact]
        public void RoundsToNearestDay_RoundDown()
        {
            // Given
            var origin = new LocalDateTime(2021, 11, 19, 11, 05, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);
            var expected = new LocalDateTime(2021, 11, 19, 00, 00, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);

            // When
            var rounded = new NearestPart(StartOfUnit.Day).Apply(origin);

            // Then
            Assert.Equal(expected, rounded);
        }

        [Fact]
        public void RoundsToNearestDay_RoundUp()
        {
            // Given
            var origin = new LocalDateTime(2021, 11, 19, 14, 05, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);
            var expected = new LocalDateTime(2021, 11, 20, 00, 00, 00).InZoneLeniently(DateTimeZoneProviders.Tzdb["Europe/Copenhagen"]);

            // When
            var rounded = new NearestPart(StartOfUnit.Day).Apply(origin);

            // Then
            Assert.Equal(expected, rounded);
        }
    }
}

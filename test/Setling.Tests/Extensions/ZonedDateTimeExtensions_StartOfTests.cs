using NodaTime;
using Xunit;

namespace Setling.Tests.Extensions
{
    public class ZonedDateTimeExtensions_StartOfTests
    {
        private static readonly DateTimeZone Timezone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/Copenhagen");

        [Fact]
        public void StartOfDay()
        {
            var origin = new LocalDateTime(2014, 11, 12, 21, 0).InZoneLeniently(Timezone);
            var result = origin.StartOf(StartOfUnit.Day);

            Assert.Equal(new LocalDateTime(2014, 11, 12, 0, 0).InZoneLeniently(Timezone), result);
        }

        [Theory]
        [InlineData(StartOfUnit.Monday, 10)]
        [InlineData(StartOfUnit.Tuesday, 11)]
        [InlineData(StartOfUnit.Wednesday, 12)]
        [InlineData(StartOfUnit.Thursday, 6)]
        [InlineData(StartOfUnit.Friday, 7)]
        [InlineData(StartOfUnit.Saturday, 8)]
        [InlineData(StartOfUnit.Sunday, 9)]
        public void StartOfWeekday(StartOfUnit weekday, int expectedDay)
        {
            var origin = new LocalDateTime(2014, 11, 12, 21, 0).InZoneLeniently(Timezone); // Wednesday
            var result = origin.StartOf(weekday);

            Assert.Equal(new LocalDateTime(2014, 11, expectedDay, 0, 0).InZoneLeniently(Timezone), result);
        }

        [Theory]
        [InlineData(StartOfUnit.January, 2014, 1)]
        [InlineData(StartOfUnit.February, 2014, 2)]
        [InlineData(StartOfUnit.March, 2014, 3)]
        [InlineData(StartOfUnit.April, 2014, 4)]
        [InlineData(StartOfUnit.May, 2014, 5)]
        [InlineData(StartOfUnit.June, 2014, 6)]
        [InlineData(StartOfUnit.July, 2014, 7)]
        [InlineData(StartOfUnit.August, 2014, 8)]
        [InlineData(StartOfUnit.September, 2014, 9)]
        [InlineData(StartOfUnit.October, 2014, 10)]
        [InlineData(StartOfUnit.November, 2014, 11)]
        [InlineData(StartOfUnit.December, 2013, 12)]
        public void StartOfMonth(StartOfUnit month, int expectedYear, int expectedMonth)
        {
            var origin = new LocalDateTime(2014, 11, 12, 21, 0).InZoneLeniently(Timezone);
            var result = origin.StartOf(month);

            Assert.Equal(new LocalDateTime(expectedYear, expectedMonth, 1, 0, 0).InZoneLeniently(Timezone), result);
        }

        [Theory]
        [InlineData(1, 2013, 12)]
        [InlineData(2, 2013, 12)]
        [InlineData(3, 2014, 3)]
        [InlineData(4, 2014, 3)]
        [InlineData(5, 2014, 3)]
        [InlineData(6, 2014, 6)]
        [InlineData(7, 2014, 6)]
        [InlineData(8, 2014, 6)]
        [InlineData(9, 2014, 9)]
        [InlineData(10, 2014, 9)]
        [InlineData(11, 2014, 9)]
        [InlineData(12, 2014, 12)]
        public void StartOfSeason(int month, int expectedYear, int expectedMonth)
        {
            var origin = new LocalDateTime(2014, month, 12, 21, 0).InZoneLeniently(Timezone);
            var result = origin.StartOf(StartOfUnit.Season);

            Assert.Equal(new LocalDateTime(expectedYear, expectedMonth, 1, 0, 0).InZoneLeniently(Timezone), result);
        }
    }
}

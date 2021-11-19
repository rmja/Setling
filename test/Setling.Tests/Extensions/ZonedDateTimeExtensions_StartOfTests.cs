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
            var result = origin.StartOf(SettleUnit.Day);

            Assert.Equal(new LocalDateTime(2014, 11, 12, 0, 0).InZoneLeniently(Timezone), result);
        }

        [Theory]
        [InlineData(SettleUnit.Monday, 10)]
        [InlineData(SettleUnit.Tuesday, 11)]
        [InlineData(SettleUnit.Wednesday, 12)]
        [InlineData(SettleUnit.Thursday, 6)]
        [InlineData(SettleUnit.Friday, 7)]
        [InlineData(SettleUnit.Saturday, 8)]
        [InlineData(SettleUnit.Sunday, 9)]
        public void StartOfWeekday(SettleUnit weekday, int expectedDay)
        {
            var origin = new LocalDateTime(2014, 11, 12, 21, 0).InZoneLeniently(Timezone); // Wednesday
            var result = origin.StartOf(weekday);

            Assert.Equal(new LocalDateTime(2014, 11, expectedDay, 0, 0).InZoneLeniently(Timezone), result);
        }

        [Theory]
        [InlineData(SettleUnit.January, 2014, 1)]
        [InlineData(SettleUnit.February, 2014, 2)]
        [InlineData(SettleUnit.March, 2014, 3)]
        [InlineData(SettleUnit.April, 2014, 4)]
        [InlineData(SettleUnit.May, 2014, 5)]
        [InlineData(SettleUnit.June, 2014, 6)]
        [InlineData(SettleUnit.July, 2014, 7)]
        [InlineData(SettleUnit.August, 2014, 8)]
        [InlineData(SettleUnit.September, 2014, 9)]
        [InlineData(SettleUnit.October, 2014, 10)]
        [InlineData(SettleUnit.November, 2014, 11)]
        [InlineData(SettleUnit.December, 2013, 12)]
        public void StartOfMonth(SettleUnit month, int expectedYear, int expectedMonth)
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
            var result = origin.StartOf(SettleUnit.Season);

            Assert.Equal(new LocalDateTime(expectedYear, expectedMonth, 1, 0, 0).InZoneLeniently(Timezone), result);
        }
    }
}

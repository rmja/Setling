using NodaTime;
using Setling.Parsers;
using Xunit;

namespace Setling.Tests.Parsers
{
    public class PeriodParserTests
    {
        [Fact]
        public void CanParseEmpty()
        {
            var period = PeriodParser.Parse(string.Empty);

            Assert.Equal(Period.Zero, period);
        }

        [Fact]
        public void CanParseWithoutTime()
        {
            var period = PeriodParser.Parse("P1Y2M3D");

            Assert.Equal(1, period.Years);
            Assert.Equal(2, period.Months);
            Assert.Equal(3, period.Days);
            Assert.Equal(0, period.Hours);
            Assert.Equal(0, period.Minutes);
            Assert.Equal(0, period.Seconds);
        }

        [Fact]
        public void CanParseWithTime()
        {
            var period = PeriodParser.Parse("P1Y2M3DT4H5M6S");

            Assert.Equal(1, period.Years);
            Assert.Equal(2, period.Months);
            Assert.Equal(3, period.Days);
            Assert.Equal(4, period.Hours);
            Assert.Equal(5, period.Minutes);
            Assert.Equal(6, period.Seconds);
        }
    }
}

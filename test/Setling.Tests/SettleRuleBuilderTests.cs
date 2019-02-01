using NodaTime;
using Xunit;

namespace Setling.Tests
{
    public class SettleRuleBuilderTests
    {
        [Fact]
        public void ShouldReturnEmptyRuleByDefault()
        {
            Assert.Equal(string.Empty, new SettleRuleBuilder().Rule.ToString());
        }

        [Fact]
        public void ShouldHandlePositiveAndNegativeOffsets()
        {
            var rule = new SettleRuleBuilder()
                .Minus(Period.FromMonths(1))
                .Plus(Period.FromYears(1))
                .Rule;

            Assert.Equal("-P1M+P1Y", rule.ToString());
        }

        [Fact]
        public void ShouldHandleRoundingAndPositiveAndNegativeOffsets()
        {
            var rule = new SettleRuleBuilder()
                .StartOf(StartOfUnit.Month)
                .Minus(Period.FromMonths(1))
                .Plus(Period.FromYears(1))
                .StartOf(StartOfUnit.Day)
                .Rule;

            Assert.Equal("month-P1M+P1Y_day", rule.ToString());
        }
    }
}

using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Setling.Tests
{
    public class SettleRuleTests
    {
        private static readonly DateTimeZone Timezone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/Copenhagen");

        [Fact]
        public void ParseEmpty()
        {
            var rule = SettleRule.Parse(string.Empty);

            Assert.Equal(new SettleRule(), rule);
        }

        [Fact]
        public void ParseStartOf()
        {
            var rule = SettleRule.Parse("day");

            Assert.Equal(new SettleRuleBuilder().StartOf(StartOfUnit.Day).Rule, rule);
        }

        [Fact]
        public void ParseStartOfWithPrefix()
        {
            var rule = SettleRule.Parse("_day");

            Assert.Equal(new SettleRuleBuilder().StartOf(StartOfUnit.Day).Rule, rule);
        }

        [Fact]
        public void ParseNearest()
        {
            var rule = SettleRule.Parse("~day");

            Assert.Equal(new SettleRuleBuilder().Nearest(StartOfUnit.Day).Rule, rule);
        }

        [Fact]
        public void ParsePeriod()
        {
            var rule = SettleRule.Parse("P1D");

            Assert.Equal(new SettleRuleBuilder().Plus(Period.FromDays(1)).Rule, rule);
        }

        [Fact]
        public void ParsePeriodWithPlus()
        {
            var rule = SettleRule.Parse("+P1D");

            Assert.Equal(new SettleRuleBuilder().Plus(Period.FromDays(1)).Rule, rule);
        }

        [Fact]
        public void ParsePeriodWithMinus()
        {
            var rule = SettleRule.Parse("-P1D");

            Assert.Equal(new SettleRuleBuilder().Minus(Period.FromDays(1)).Rule, rule);
        }

        [Fact]
        public void ParseStartOfWithPeriod()
        {
            var rule = SettleRule.Parse("day+P1D");

            Assert.Equal(new SettleRuleBuilder().StartOf(StartOfUnit.Day).Plus(Period.FromDays(1)).Rule, rule);
        }

        [Fact]
        public void ParseStartOfWithMultiplePeriods()
        {
            var rule = SettleRule.Parse("day+P1D-PT2H");

            Assert.Equal(new SettleRuleBuilder().StartOf(StartOfUnit.Day).Plus(Period.FromDays(1)).Minus(Period.FromHours(2)).Rule, rule);
        }

        [Fact]
        public void ParseWithZeroPeriodAndDiscard()
        {
            var rule = SettleRule.Parse("day-P0D+PT0H");

            Assert.Equal("day", rule.ToString());
            Assert.Equal(new SettleRuleBuilder().StartOf(StartOfUnit.Day).Rule, rule);
        }

        [Fact]
        public void SettleNoParts()
        {
            var origin = new LocalDateTime(2014, 11, 12, 21, 0).InZoneLeniently(Timezone);
            var settled = SettleRule.Parse(string.Empty).Settle(origin);

            Assert.Equal(origin, settled);
        }

        [Fact]
        public void SettleShouldApplyParts()
        {
            var origin = new LocalDateTime(2014, 11, 12, 21, 0).InZoneLeniently(Timezone);
            var settled = SettleRule.Parse("day+P2D-PT1H").Settle(origin);

            Assert.Equal(new LocalDateTime(2014, 11, 13, 23, 0).InZoneLeniently(Timezone), settled);
        }

        [Theory]
        [InlineData(10, 31, 1)]
        [InlineData(10, 31, 14)]
        [InlineData(10, 31, 15)]
        [InlineData(11, 30, 16)]
        [InlineData(11, 30, 30)]
        public void SettleExpectedReadingDate(int expectedMonth, int expectedDay, int day)
        {
            var origin = new LocalDateTime(2014, 11, day, 21, 0).InZoneLeniently(Timezone);
            var settled = SettleRule.Parse("-P15D_month+P1M-P1D").Settle(origin);

            Assert.Equal(new LocalDateTime(2014, expectedMonth, expectedDay, 0, 0).InZoneLeniently(Timezone), settled);
        }
    }
}

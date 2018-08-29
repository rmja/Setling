using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Setling.Internal
{
    public class PeriodOffsetPart : IPart, IEquatable<PeriodOffsetPart>
    {
        public int Sign { get; }
        public Period Period { get; }

        public PeriodOffsetPart(int sign, Period duration)
        {
            Sign = sign;
            Period = duration;
        }

        public ZonedDateTime Apply(ZonedDateTime origin)
        {
            var local = origin.LocalDateTime;

            var applied = Sign > 0 ? local.Plus(Period) : Sign < 0 ? local.Minus(Period) : local;

            return origin.Zone.AtLeniently(applied);
        }

        public string ToRuleString(bool prefixWithSeparator)
        {
            if (Period == Period.Zero)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            if (prefixWithSeparator)
            {
                builder.Append(Sign > 0 ? "+" : "-");
            }

            builder.Append("P");

            if (Period.Years > 0)
            {
                builder.Append(Period.Years);
                builder.Append("Y");
            }

            if (Period.Months > 0)
            {
                builder.Append(Period.Months);
                builder.Append("M");
            }

            if (Period.Days > 0)
            {
                builder.Append(Period.Days);
                builder.Append("D");
            }

            if (Period.Hours + Period.Minutes + Period.Seconds > 0)
            {
                builder.Append("T");

                if (Period.Hours > 0)
                {
                    builder.Append(Period.Hours);
                    builder.Append("H");
                }

                if (Period.Minutes > 0)
                {
                    builder.Append(Period.Minutes);
                    builder.Append("M");
                }

                if (Period.Seconds > 0)
                {
                    builder.Append(Period.Seconds);
                    builder.Append("S");
                }
            }

            return builder.ToString();
        }

        public bool Equals(PeriodOffsetPart other) =>
            Sign == other.Sign &&
            EqualityComparer<Period>.Default.Equals(Period, other.Period);

        public override bool Equals(object obj) => obj is PeriodOffsetPart other && Equals(other);

        public override int GetHashCode() => (Sign, Period).GetHashCode();
    }
}

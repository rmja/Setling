using NodaTime;
using System;
using System.Text;

namespace Setling.Parts
{
    public class OffsetPart : IPart, IEquatable<OffsetPart>
    {
        public int Sign { get; }
        public Period Period { get; }

        public OffsetPart(int sign, Period period)
        {
            Sign = sign;
            Period = period;
        }

        public ZonedDateTime Apply(ZonedDateTime origin)
        {
            var local = origin.LocalDateTime;

            var applied = Sign > 0 ? local.Plus(Period) : Sign < 0 ? local.Minus(Period) : local;

            return origin.Zone.AtLeniently(applied);
        }

        public string ToRuleString(bool forcePrefixWithSeparator)
        {
            var builder = new StringBuilder();

            if (Sign < 0)
            {
                builder.Append('-');
            }
            else if (forcePrefixWithSeparator)
            {
                builder.Append('+');
            }

            builder.Append('P');

            if (Period.Years > 0)
            {
                builder.Append(Period.Years);
                builder.Append('Y');
            }

            if (Period.Months > 0)
            {
                builder.Append(Period.Months);
                builder.Append('M');
            }

            if (Period.Days > 0)
            {
                builder.Append(Period.Days);
                builder.Append('D');
            }

            if (Period.Hours + Period.Minutes + Period.Seconds > 0)
            {
                builder.Append('T');

                if (Period.Hours > 0)
                {
                    builder.Append(Period.Hours);
                    builder.Append('H');
                }

                if (Period.Minutes > 0)
                {
                    builder.Append(Period.Minutes);
                    builder.Append('M');
                }

                if (Period.Seconds > 0)
                {
                    builder.Append(Period.Seconds);
                    builder.Append('S');
                }
            }

            return builder.ToString();
        }

        public bool Equals(OffsetPart other) =>
            Sign == other.Sign &&
            Period.Equals(other.Period);

        public override bool Equals(object obj) => obj is OffsetPart other && Equals(other);

        public override int GetHashCode() => (Sign, Period).GetHashCode();
    }
}

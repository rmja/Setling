using NodaTime;
using Setling.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Setling
{
    public class SettleRule : IEquatable<SettleRule>
    {
        private static readonly Regex _firstRegex = new Regex(@"^([_+-])?([a-zA-Z0-9]+)(.*)", RegexOptions.Compiled);
        private static readonly Regex _remainingRegex = new Regex(@"^([_+-])([a-zA-Z0-9]+)(.*)", RegexOptions.Compiled);

        internal List<IPart> Parts { get; } = new List<IPart>();

        public static SettleRule Parse(string input)
        {
            var builder = new SettleRuleBuilder();

            var match = _firstRegex.Match(input);

            if (match.Success)
            {
                AddPart(match.Groups[1].Value.NullIfEmpty() ?? (match.Groups[2].Value[0] == 'P' ? "+" : "_"), match.Groups[2].Value);

                var remaining = match.Groups[3].Value;
                while ((match = _remainingRegex.Match(remaining)).Success)
                {
                    AddPart(match.Groups[1].Value, match.Groups[2].Value);
                    remaining = match.Groups[3].Value;
                }
            }

            return builder.Rule;

            void AddPart(string op, string value)
            {
                switch (op)
                {
                    case "_":
                        builder.StartOf(StartOfUnitEx.Parse(value));
                        break;
                    case "+":
                        {
                            var period = PeriodEx.Parse(value);
                            if (!period.Equals(Period.Zero))
                            {
                                builder.Plus(period);
                            }
                        }
                        break;
                    case "-":
                        {
                            var period = PeriodEx.Parse(value);
                            if (!period.Equals(Period.Zero))
                            {
                                builder.Minus(period);
                            }
                        }
                        break;
                }
            }
        }

        public ZonedDateTime Settle(ZonedDateTime origin)
        {
            foreach (var part in Parts)
            {
                origin = part.Apply(origin);
            }

            return origin;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var part in Parts)
            {
                builder.Append(part.ToRuleString(builder.Length > 0));
            }

            return builder.ToString();
        }

        public bool Equals(SettleRule other) => Enumerable.SequenceEqual(Parts, other.Parts);

        public override bool Equals(object obj) => obj is SettleRule other && Equals(other);

        public override int GetHashCode() => Parts.Count;
    }
}

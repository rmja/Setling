using NodaTime;
using System;

namespace Setling.Parts
{
    public class StartOfPart : IPart, IEquatable<StartOfPart>
    {
        public StartOfUnit Unit { get; }

        public StartOfPart(StartOfUnit unit)
        {
            Unit = unit;
        }

        public ZonedDateTime Apply(ZonedDateTime origin) => origin.StartOf(Unit);

        public string ToRuleString(bool forcePrefixWithSeparator)
        {
            var unit = Unit.ToString();

            return (forcePrefixWithSeparator ? "_" : string.Empty) + char.ToLowerInvariant(unit[0]) + unit.Substring(1);
        }

        public bool Equals(StartOfPart other) => Unit == other.Unit;

        public override bool Equals(object obj) => obj is StartOfPart other && Equals(other);

        public override int GetHashCode() => Unit.GetHashCode();
    }
}

using NodaTime;
using System;

namespace Setling.Parts
{
    public class StartOfPart : IPart, IEquatable<StartOfPart>
    {
        public SettleUnit Unit { get; }

        public StartOfPart(SettleUnit unit)
        {
            Unit = unit;
        }

        public ZonedDateTime Apply(ZonedDateTime origin) => origin.StartOf(Unit);

        public string ToRuleString(bool forcePrefixWithSeparator) => (forcePrefixWithSeparator ? "_" : string.Empty) + Unit.ToCamelCaseString();

        public bool Equals(StartOfPart other) => Unit == other.Unit;

        public override bool Equals(object obj) => obj is StartOfPart other && Equals(other);

        public override int GetHashCode() => Unit.GetHashCode();
    }
}

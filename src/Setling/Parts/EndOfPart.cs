using NodaTime;
using System;

namespace Setling.Parts
{
    internal class EndOfPart : IPart, IEquatable<EndOfPart>
    {
        public SettleUnit Unit { get; }

        public EndOfPart(SettleUnit unit)
        {
            Unit = unit;
        }

        public ZonedDateTime Apply(ZonedDateTime origin) => origin.EndOf(Unit);

        public string ToRuleString(bool forcePrefixWithSeparator) => "^" + Unit.ToCamelCaseString();

        public bool Equals(EndOfPart other) => Unit == other.Unit;

        public override bool Equals(object obj) => obj is EndOfPart other && Equals(other);

        public override int GetHashCode() => Unit.GetHashCode();
    }
}

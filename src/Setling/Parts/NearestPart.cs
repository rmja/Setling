using NodaTime;
using System;

namespace Setling.Parts
{
    internal class NearestPart : IPart, IEquatable<NearestPart>
    {
        public SettleUnit Unit { get; }

        public NearestPart(SettleUnit unit)
        {
            Unit = unit;
        }

        public ZonedDateTime Apply(ZonedDateTime origin)
        {
            var startOf = origin.StartOf(Unit);
            var endOf = origin.EndOf(Unit);
            if (origin - startOf <= endOf - origin)
            {
                return startOf;
            }
            else
            {
                return endOf;
            }
        }

        public string ToRuleString(bool forcePrefixWithSeparator) => "~" + Unit.ToCamelCaseString();

        public bool Equals(NearestPart other) => Unit == other.Unit;

        public override bool Equals(object obj) => obj is NearestPart other && Equals(other);

        public override int GetHashCode() => Unit.GetHashCode();
    }
}

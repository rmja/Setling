using NodaTime;
using System;

namespace Setling.Parts
{
    public class NearestPart : IPart, IEquatable<NearestPart>
    {
        public StartOfUnit Unit { get; }

        public NearestPart(StartOfUnit unit)
        {
            Unit = unit;
        }

        public ZonedDateTime Apply(ZonedDateTime origin)
        {
            var earliest = origin.StartOf(Unit);

            var latest = Unit switch
            {
                StartOfUnit.Second => earliest.PlusSeconds(1),
                StartOfUnit.Minute => earliest.PlusMinutes(1),
                StartOfUnit.Hour => earliest.PlusHours(1),
                StartOfUnit.Day => earliest.PlusDaysLeniently(1),
                StartOfUnit.Month => earliest.PlusMonthsLeniently(1),
                StartOfUnit.Quarter => earliest.PlusMonthsLeniently(3),
                StartOfUnit.Year => earliest.PlusYearsLeniently(1),
                StartOfUnit.Monday or
                StartOfUnit.Tuesday or
                StartOfUnit.Wednesday or
                StartOfUnit.Thursday or
                StartOfUnit.Friday or
                StartOfUnit.Saturday or
                StartOfUnit.Sunday => earliest.PlusDaysLeniently(7),
                StartOfUnit.January or
                StartOfUnit.February or
                StartOfUnit.March or
                StartOfUnit.April or
                StartOfUnit.May or
                StartOfUnit.June or
                StartOfUnit.July or
                StartOfUnit.August or
                StartOfUnit.September or
                StartOfUnit.October or
                StartOfUnit.November or
                StartOfUnit.December => earliest.PlusYearsLeniently(1),
                StartOfUnit.Season => earliest.PlusMonthsLeniently(3),
                StartOfUnit.Spring or
                StartOfUnit.Summer or
                StartOfUnit.Autumn or
                StartOfUnit.Winter => earliest.PlusYearsLeniently(1),
                _ => throw new InvalidOperationException(),
            };

            var earliestDiff = origin - earliest;
            var latestDiff = latest - origin;
            if (earliestDiff <= latestDiff)
            {
                return earliest;
            }
            else
            {
                return latest;
            }
        }

        public string ToRuleString(bool forcePrefixWithSeparator)
        {
            var unit = Unit.ToString();

            return "~" + char.ToLowerInvariant(unit[0]) + unit.Substring(1);
        }

        public bool Equals(NearestPart other) => Unit == other.Unit;

        public override bool Equals(object obj) => obj is NearestPart other && Equals(other);

        public override int GetHashCode() => Unit.GetHashCode();
    }
}

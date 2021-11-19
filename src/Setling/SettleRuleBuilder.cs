using NodaTime;
using Setling.Parts;

namespace Setling
{
    public class SettleRuleBuilder
    {
        public SettleRule Rule { get; } = new SettleRule();

        public SettleRuleBuilder StartOf(StartOfUnit unit)
        {
            Rule.Parts.Add(new StartOfPart(unit));
            return this;
        }

        public SettleRuleBuilder Nearest(StartOfUnit unit)
        {
            Rule.Parts.Add(new NearestPart(unit));
            return this;
        }

        public SettleRuleBuilder Plus(Period period)
        {
            Rule.Parts.Add(new PeriodOffsetPart(1, period));
            return this;
        }

        public SettleRuleBuilder Minus(Period period)
        {
            Rule.Parts.Add(new PeriodOffsetPart(-1, period));
            return this;
        }
    }
}

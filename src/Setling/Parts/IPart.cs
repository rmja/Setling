using NodaTime;

namespace Setling.Parts
{
    internal interface IPart
    {
        ZonedDateTime Apply(ZonedDateTime origin);
        string ToRuleString(bool forcePrefixWithSeparator);
    }
}

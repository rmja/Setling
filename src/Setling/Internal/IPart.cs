using NodaTime;

namespace Setling.Internal
{
    internal interface IPart
    {
        ZonedDateTime Apply(ZonedDateTime origin);
        string ToRuleString(bool forcePrefixWithSeparator);
    }
}

using NodaTime;

namespace Setling.Parts
{
    public interface IPart
    {
        ZonedDateTime Apply(ZonedDateTime origin);
        string ToRuleString(bool forcePrefixWithSeparator);
    }
}

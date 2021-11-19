using System;

namespace Setling.Parsers
{
    public static class SettleUnitParser
    {
        public static SettleUnit Parse(string input) => Enum.Parse<SettleUnit>(input, true);
    }
}

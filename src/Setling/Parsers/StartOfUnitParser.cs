using System;

namespace Setling.Parsers
{
    public static class StartOfUnitParser
    {
        public static StartOfUnit Parse(string input) => Enum.Parse<StartOfUnit>(input, true);
    }
}

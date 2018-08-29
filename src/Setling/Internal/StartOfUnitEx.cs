using System;

namespace Setling.Internal
{
    public static class StartOfUnitEx
    {
        public static StartOfUnit Parse(string input) => (StartOfUnit)Enum.Parse(typeof(StartOfUnit), input, true);
    }
}

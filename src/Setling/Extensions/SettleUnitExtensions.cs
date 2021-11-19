namespace Setling
{
    internal static class SettleUnitExtensions
    {
        public static string ToCamelCaseString(this SettleUnit unit)
        {
            var @string = unit.ToString();
            return char.ToLowerInvariant(@string[0]) + @string[1..];
        }
    }
}

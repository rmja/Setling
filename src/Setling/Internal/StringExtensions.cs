namespace Setling
{
    internal static class StringExtensions
    {
        public static string NullIfEmpty(this string input) => string.IsNullOrEmpty(input) ? null : input;
    }
}

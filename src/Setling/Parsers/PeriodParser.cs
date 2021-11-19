using NodaTime;
using System;
using System.Text.RegularExpressions;

namespace Setling.Parsers
{
    public static class PeriodParser
    {
        private static class Groups
        {
            public const string Years = nameof(Years);
            public const string Months = nameof(Months);
            public const string Days = nameof(Days);
            public const string Hours = nameof(Hours);
            public const string Minutes = nameof(Minutes);
            public const string Seconds = nameof(Seconds);
        }

        private static readonly Regex _regex = new($@"^P
((?<{Groups.Years}>\d+)Y)?
((?<{Groups.Months}>\d+)M)?
((?<{Groups.Days}>\d+)D)?
(T
    ((?<{Groups.Hours}>\d+)H)?
    ((?<{Groups.Minutes}>\d+)M)?
    ((?<{Groups.Seconds}>\d+)S)?
)?$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static Period Parse(string input)
        {
            if (input == string.Empty)
            {
                return Period.Zero;
            }

            var builder = new PeriodBuilder();
            var match = _regex.Match(input);
            if (match.Success)
            {
                builder.Years += int.Parse(match.Groups[Groups.Years].Value.NullIfEmpty() ?? "0");
                builder.Months += int.Parse(match.Groups[Groups.Months].Value.NullIfEmpty() ?? "0");
                builder.Days += int.Parse(match.Groups[Groups.Days].Value.NullIfEmpty() ?? "0");
                builder.Hours += int.Parse(match.Groups[Groups.Hours].Value.NullIfEmpty() ?? "0");
                builder.Minutes += int.Parse(match.Groups[Groups.Minutes].Value.NullIfEmpty() ?? "0");
                builder.Seconds += int.Parse(match.Groups[Groups.Seconds].Value.NullIfEmpty() ?? "0");
            }
            else
            {
                throw new ArgumentException("Input is not a valid period", nameof(input));
            }

            return builder.Build();
        }
    }
}

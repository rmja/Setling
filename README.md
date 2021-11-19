# Setling
A time settle library for NodaTime.

# Usage

```C#
var timezone = DateTimeZoneProviders.Tzdb["Europe/Copenhagen"];
var now = DateTimeOffset.UtcNow.ToZonedDateTime().WithZone(timezone);

var rule = SettleRule.Parse("day+PT2H"); // Equivalent to _day+PT2H
var startOfTodayPlusTwoHours = rule.Settle(now);

var rule = SettleRule.Parse("^day+PT2H");
var startOfTomorrowPlusTwoHours = rule.Settle(now);

var rule = SettleRule.Parse("~day+PT2H");
var startOfTodayOrTomorrowPlusTwoHours = rule.Settle(now);
```

The rule string can consist of any of the following parts:
* A rounding to start of `_`, end of `^`, or nearest `~` of any of the intervals:
`second`,
`minute`,
`hour`,
`day`,
`month`,
`quarter`,
`year`,
`monday`,
`tuesday`,
`wednesday`,
`thursday`,
`friday`,
`saturday`,
`sunday`,
`january`,
`february`,
`march`,
`april`,
`may`,
`june`,
`july`,
`august`,
`september`,
`october`,
`november`,
`december`,
`season`,
`spring`,
`summer`,
`autumn`,
`winter`,
* Plus/minus a period according to ISO8601, e.g. PT2H (two hours)

The parts can be chained in any way, for examle: `day+PT2H_month~year-P7D`. This will take the input time through the following process: Find the start of the day, add two hours, find the start of the month, find the nearest year, subtract 7 days.

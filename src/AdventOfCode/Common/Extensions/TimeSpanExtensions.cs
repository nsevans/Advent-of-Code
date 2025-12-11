using System;

namespace AdventOfCode.Common.Extensions;

public static class TimeSpanExtensions
{
    /// <summary>
    /// Get a formatted string containing the given timespan in milliseconds with left padding and decimal rounding.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <param name="padding"></param>
    /// <param name="roundToDecimal"></param>
    /// <returns></returns>
	public static string ToFormattedMilliseconds(this TimeSpan timeSpan, int padding = 0, int roundToDecimal = 4)
    {
        var roundedMilliseconds = timeSpan.ToRoundedMilliseconds(roundToDecimal);
        return roundedMilliseconds.PadLeft(padding);
    }

    /// <summary>
    /// Get the given timespan in milliseconds rounded to the given decimal point.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <param name="roundToDecimal"></param>
    /// <returns></returns>
	public static string ToRoundedMilliseconds(this TimeSpan timeSpan, int roundToDecimal)
    {
        return timeSpan.TotalMilliseconds.ToString($"#0.{new string('0', roundToDecimal)}");
    }
}
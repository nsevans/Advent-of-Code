using System;

namespace AdventOfCode.Common.Extensions;

public static class TimeSpanExtensions
{
	public static string ToFormattedMilliseconds(this TimeSpan timeSpan, int padding = 0, int roundToDecimal = 4)
	{
		var roundedMilliseconds = timeSpan.ToRoundedMilliseconds(roundToDecimal);
		return roundedMilliseconds.PadLeft(padding);
	}

	public static string ToRoundedMilliseconds(this TimeSpan timeSpan, int roundToDecimal)
	{
		return timeSpan.TotalMilliseconds.ToString($"#0.{new string('0', roundToDecimal)}");
	}
}
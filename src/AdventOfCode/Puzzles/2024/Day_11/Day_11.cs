using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_11;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/11
///
/// Example/Test Input:
/// 	125 17
/// </summary>
public abstract class Day_11 : BaseCSharpSolver
{
	public override string Title => "Plutonian Pebbles";
	public override int Day => 11;
	public override int Year => 2024;

	protected static List<long> ParseStones(IEnumerable<string> input)
	{
		return input
			.SelectMany(x => x
				.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(x => long.Parse(x)))
			.ToList();

	}
}
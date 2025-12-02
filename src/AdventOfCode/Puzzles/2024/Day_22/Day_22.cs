using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_22;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/22
///
/// Example/Test Input:
///		1
///		10
///		100
///		2024
/// </summary>
public abstract class Day_22 : BaseCSharpSolver
{
	public override string Title => "Monkey Market";
	public override int Day => 22;
	public override int Year => 2024;

	protected static List<long> ParseSecretNumbers(List<string> input)
	{
		return input.Select(long.Parse).ToList();
	}
}
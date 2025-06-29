using System;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_03;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/3
/// 
/// Example/Test Input: 
/// 	xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
/// </summary>
public abstract class Day_03 : BaseSolver
{
	public override string Title => "Mull It Over";
	public override int Day => 3;
	public override int Year => 2024;

	protected static long GetProductOfValuesFromMatch(string match)
	{
		// Strip out all non digit characters from "mul(xxx,xxx)"
		var values = match
			.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries)
			.Select(x => long.Parse(x))
			.ToList();
			
		return values[0] * values[1];
	}
}
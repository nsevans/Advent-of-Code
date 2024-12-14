using System;
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_01;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/1
/// 
/// Input Format: 
/// 	3   4
/// 	4   3
/// 	2   5
/// 	1   3
/// 	3   9
/// 	3   3
/// </summary>
public abstract class Day_01 : BaseSolver
{
	public override string Title => "Historian Hysteria";
    public override int Day => 1;
    public override int Year => 2024;

	protected (List<long>, List<long>) ParsePairs(IEnumerable<string> input)
	{
		var left = new List<long>();
		var right = new List<long>();

		foreach(var line in input)
		{
			var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			left.Add(long.Parse(numbers[0]));
			right.Add(long.Parse(numbers[1]));
		}

		return (left, right);
	}
}
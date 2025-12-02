using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_07;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/7
///
/// Example/Test Input:
/// 	190: 10 19
///		3267: 81 40 27
///		83: 17 5
///		156: 15 6
///		7290: 6 8 6 15
///		161011: 16 10 13
///		192: 17 8 14
///		21037: 9 7 18 13
///		292: 11 6 16 20
/// </summary>
public abstract class Day_07 : BaseCSharpSolver
{
	public override string Title => "Bridge Repair";
	public override int Day => 7;
	public override int Year => 2024;

	protected enum Operator
	{
		Add, Multiply, Concatenate
	}

	protected static Dictionary<long, List<List<long>>> ParseEquationDictionary(IEnumerable<string> input)
	{
		var dictionary = new Dictionary<long, List<List<long>>>();

		foreach (var line in input)
		{
			var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			var key = long.Parse(split[0]);
			var numbers = split[1]
				.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(x => long.Parse(x))
				.ToList();

			if (dictionary.TryGetValue(key, out var value))
				value.Add(numbers);
			else
				dictionary.Add(key, [ numbers ]);
		}

		return dictionary;
	}
}
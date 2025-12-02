using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_05;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/5
///
/// Example/Test Input:
/// 	47|53
///		97|13
///		97|61
///		97|47
///		75|29
///		61|13
///		75|53
///		29|13
///		97|29
///		53|29
///		61|53
///		97|53
///		61|29
///		47|13
///		75|47
///		97|75
///		47|61
///		75|61
///		47|29
///		75|13
///		53|13
///
///		75,47,61,53,29
///		97,61,53,29,13
///		75,29,13
///		75,97,47,61,53
///		61,13,29
///		97,13,75,29,47
/// </summary>
public abstract class Day_05 : BaseCSharpSolver
{
	public override string Title => "Print Queue";
	public override int Day => 5;
	public override int Year => 2024;

	protected static Dictionary<int, List<int>> ParsePageOrderingRules(IEnumerable<string> input)
	{
		var result = new Dictionary<int, List<int>>();

		foreach (var line in input)
		{
			// Stop before hitting the pages to produce
			if (string.IsNullOrWhiteSpace(line))
				break;

			var key = int.Parse(line.Split('|', StringSplitOptions.RemoveEmptyEntries)[0]);
			var value = int.Parse(line.Split('|', StringSplitOptions.RemoveEmptyEntries)[1]);

			if (result.ContainsKey(key))
				result[key].Add(value);
			else
				result.Add(key, [ value ]);
		}

		return result;
	}

	protected static List<List<int>> ParsePagesToProduce(IEnumerable<string> input)
	{
		var result = new List<List<int>>();

		foreach (var line in input)
		{
			// Skip all lines that are for the page ordering rules
			if (line.Contains('|') || string.IsNullOrWhiteSpace(line))
				continue;

			var lineValues = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(x => int.Parse(x))
				.ToList();

			result.Add(lineValues);
		}
		return result;
	}
}
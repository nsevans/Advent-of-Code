using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_05;

/// <summary>
/// Link: https://adventofcode.com/2024/day/5
/// Input Format:
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
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 05 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfValidMiddlePageNumbers(input);
		Console.WriteLine(resultPart1);

		var resultPart2 = CalculateSumOfInvalidMiddlePageNumbers(input);
		Console.WriteLine(resultPart2);
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private static Dictionary<int, List<int>> ParsePageOrderingRules(IEnumerable<string> input)
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

	private static List<List<int>> ParsePagesToProduce(IEnumerable<string> input)
	{
		var result = new List<List<int>>();

		foreach (var line in input)
		{
			// Skip all lines that are for the page ordering rules
			if (line.Contains('|') || string.IsNullOrWhiteSpace(line))
				continue;

			var lineValues = line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
			result.Add(lineValues);
		}
		return result;
	}

	#region PART 1
	private static int CalculateSumOfValidMiddlePageNumbers(IEnumerable<string> input)
	{
		var sum = 0;

		var orderingRules = ParsePageOrderingRules(input);
		var pagesToProduce = ParsePagesToProduce(input);

		foreach (var update in pagesToProduce)
		{
			var isValid = true;
			foreach (var (page, index) in update.Select((p, i) => (p, i)))
			{
				// Scan preceding pages to see if all are valid within the current pages ordering rules
				var nextPages = update[(index + 1)..];
				if (!nextPages.All(n => orderingRules[page].Contains(n)))
				{
					isValid = false;
					break;
				}
			}

			if (isValid)
				sum += update[update.Count / 2];
		}

		return sum;
	}
	#endregion
	
	#region PART 2
	private static int CalculateSumOfInvalidMiddlePageNumbers(IEnumerable<string> input)
	{
		var sum = 0;

		var orderingRules = ParsePageOrderingRules(input);
		var pagesToProduce = ParsePagesToProduce(input);

		foreach (var update in pagesToProduce)
		{
			// Copy of the current update so it can be safely modified
			var currentUpdateCopy = new List<int>(update);
			var newUpdate = new List<int>();

			for (var i = 0; i < currentUpdateCopy.Count; i++)
			{
				var page = currentUpdateCopy[i];
				var pageOrderingRules = orderingRules[page];

				// Scan preceding pages to see if all are valid within the current pages ordering rules
				if(!currentUpdateCopy[(i + 1)..].All(p => pageOrderingRules.Contains(p)))
					// If it's not valid add it to the end of the list so it can be re evaluated / evaluated
					// with the following pages
					currentUpdateCopy.Add(page);
				else
					newUpdate.Add(page);
			}
			
			if (!update.SequenceEqual(newUpdate))
				sum += newUpdate[newUpdate.Count / 2];
		}

		return sum;
	}
	#endregion
}
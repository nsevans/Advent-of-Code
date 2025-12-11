using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_19;

public class Part_01 : Day_19
{
	public override int Part => 1;

	public override string ResultMessage => "Sum of all possible valid designs";

	private List<string> _patterns;
	private List<string> _designs;

	public override void PrepareData(List<string> input)
	{
		_patterns = ParseTowelPatterns(input);
		_designs = ParseTowelDesigns(input);
	}

	private static List<string> ParseTowelPatterns(List<string> input)
	{
		return input[0]
			.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.OrderByDescending(t => t.Length)
			.ToList();
	}

	private static List<string> ParseTowelDesigns(List<string> input)
	{
		return input[2..];
	}

	public override string GetResult()
	{
		return CalculateNumberOfPossibleDesigns(_patterns, _designs).ToString();
	}

	private static int CalculateNumberOfPossibleDesigns(List<string> patterns, List<string> designs)
	{
		var sum = 0;

		foreach (var design in designs)
		{
			var possibleStartingPatterns = patterns.Where(p => design.StartsWith(p));

			foreach (var startingPattern in possibleStartingPatterns)
			{
				if (IsDesignPossibleRecursive(design, startingPattern, patterns))
				{
					sum++;
					break;
				}
			}
		}

		return sum;
	}

	private static bool IsDesignPossibleRecursive(string design, string currentPattern, List<string> patterns)
	{
		var remainingDesign = design.Substring(currentPattern.Length);

		if (remainingDesign == "")
			return true;

		var possibleNextPatterns = patterns.Where(p => remainingDesign.StartsWith(p));

		foreach (var nextPattern in possibleNextPatterns)
		{
			if (IsDesignPossibleRecursive(remainingDesign, nextPattern, patterns))
			{
				return true;
			}
		}

		return false;
	}
}
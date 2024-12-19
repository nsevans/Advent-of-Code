using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_19;

public class Part_02 : Day_19
{
	public override int Part => 2;

	private List<string> _patterns;
	private List<string> _designs;

	public override void PrepareData(List<string> input)
	{
		_patterns = ParseTowelPatterns(input);
		_designs = ParseTowelDesigns(input);
	}

	private List<string> ParseTowelPatterns(List<string> input)
	{
		return input[0]
			.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.OrderByDescending(t => t.Length)
			.ToList();
	}

	private List<string> ParseTowelDesigns(List<string> input)
	{
		return input[2..];
	}

	public override void Solve()
	{
		var result = CalculateNumberOfPossibleDesigns(_patterns, _designs);
		Console.WriteLine($"Sum of all possible pattern combinations for each design: {result}");
	}

	private long CalculateNumberOfPossibleDesigns(List<string> patterns, List<string> designs)
	{
		var cache = new Dictionary<string, long>();
		var result = 0L;

		foreach (var design in designs)
		{
			result += CalculateNumberOfPossiblePatternCombinationsRecursive(design, patterns, cache);
		}

		return result;
	}

	private long CalculateNumberOfPossiblePatternCombinationsRecursive(string design, List<string> patterns, Dictionary<string, long> cache)
	{
		if (cache.TryGetValue(design, out var cachedValue))
			return cachedValue;

		if (design == "")
			return 1L;

		var options = 0L;
		foreach (var pattern in patterns)
		{
			if (!design.StartsWith(pattern))
				continue;

			options += CalculateNumberOfPossiblePatternCombinationsRecursive(design.Substring(pattern.Length), patterns, cache);
		}

		cache.TryAdd(design, options);

		return options;
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Day_05;

public class Part_01 : Day_05
{
	public override int Part => 1;
	
	private Dictionary<int, List<Mapping>> _mappingDictionary;
	private List<long> _seeds;

    public override void PrepareData(List<string> input)
    {
		_mappingDictionary = ParseMappings(input);
		_seeds = ParseSeeds(input);
    }

	private static List<long> ParseSeeds(List<string> input)
	{
		return input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]
			.Select(x => long.Parse(x))
			.ToList();
	}

    public override void Solve()
    {
		var result = CalculateLowestLocation(_mappingDictionary, _seeds);
		Console.WriteLine($"Lowest location value with seed range: {result}");
	}

	private static long CalculateLowestLocation(Dictionary<int, List<Mapping>> mappingsDictionary, List<long> seeds)
	{
		var minLocation = long.MaxValue;

		foreach(var seed in seeds)
		{
			var seedLocation = CalculateLocationRecursive(seed, mappingsDictionary, 0);

			if (seedLocation < minLocation)
				minLocation = seedLocation;
		}

		return minLocation;
	}

	private static long CalculateLocationRecursive(long sourceValue, Dictionary<int, List<Mapping>> mappingsDictionary, int level)
	{
		var currentMappings = mappingsDictionary[level];
		// Set so if no range is found it defaults to source value
		var destinationValue = sourceValue;
		foreach(var mapping in currentMappings)
		{
			if (sourceValue >= mapping.Range.Start && sourceValue <= mapping.Range.End)
			{
				// Set the destination value to the start of the destination range added to the
				// difference between the source value and the start of the source range
				destinationValue = mapping.Range.Start + mapping.Offset + (sourceValue - mapping.Range.Start);
				break;
			}
		}

		var nextLevel = level + 1;
		// When we've reached the last level and determined the value, return it back up the chain
		if (nextLevel >= mappingsDictionary.Count)
			return destinationValue;

		return CalculateLocationRecursive(destinationValue, mappingsDictionary, nextLevel);
	}
}
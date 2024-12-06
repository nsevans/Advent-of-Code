using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2023.Puzzle_05;

/// <summary>
/// Link: https://adventofcode.com/2023/day/5
/// Input Format:
///		seeds: 3136945476 509728956 1904897211
///
///		seed-to-soil map:
///		2122609492 2788703865 117293332
///
/// 	soil-to-fertilizer map:
/// 	1839905294 2992775329 34548650
///
/// 	...
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2023 - Puzzle 05 ###\n");
		var input = GetInput("./input.txt");
		
		var resultPart1 = CalculateLowestLocation(input);
		Console.WriteLine($"[PART 1] Lowest location value with seed: {resultPart1}");

		var resultPart2 = CalculateLowestLocationFromRanges(input);
		Console.WriteLine($"[PART 2] Lowest location value with seed range: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private record Mapping(long RangeStart, long RangeEnd, long Offset)
	{
		public ValueRange Range => new ValueRange(RangeStart, RangeEnd);
	};

	private static Dictionary<int, List<Mapping>> ParseMappings(List<string> input)
	{
		var mappingDictionary = new Dictionary<int, List<Mapping>>();

		var currentMappingList = -1;
		for (int i = 2; i < input.Count; i++)
		{
			// If line is empty, skip
			if (string.IsNullOrWhiteSpace(input[i]))
				continue;

			// If previous line was empty, new section is started
			if (string.IsNullOrWhiteSpace(input[i - 1]))
			{
				currentMappingList++;
				mappingDictionary.Add(currentMappingList, new List<Mapping>());
			}
			else
			{
				var mappingValues = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries)
					.Select(x => long.Parse(x))
					.ToList();
				mappingDictionary[currentMappingList].Add(new Mapping(mappingValues[1], mappingValues[1] + mappingValues[2] - 1, mappingValues[0] - mappingValues[1]));
			}
		}

		return mappingDictionary;
	}

	#region PART 1
	private static List<long> ParseSeeds(List<string> input)
	{
		return input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]
			.Select(x => long.Parse(x))
			.ToList();
	}

	private static long CalculateLowestLocation(List<string> input)
	{
		var mappingsDictionary = ParseMappings(input);
		var seeds = ParseSeeds(input);

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

	#endregion

	#region PART 2
	private record ValueRange(long Start, long End)
	{
		public bool Contains(ValueRange other) => other.Start >= Start && other.End <= End;

		public ValueRange GetIntersection(ValueRange other)
		{
			// If this range is completely contained within the other range
			// other: 	|------------------|
			// this:		|--------|
			// result:		|--------|
			if (other.Contains(this))
				return new ValueRange(Start, End);

			// If the other range is completely contained within this range
			// other:		|--------|
			// this: 	|------------------|
			// result:		|--------|
			if (Contains(other))
				return new ValueRange(other.Start, other.End);

			// If the start of the other range is outside (below) this range but the end of the other range
			// is within this range
			// other: 	|----------|
			// this:		|----------|
			// result:		|------|
			if (other.Start <= Start && other.End >= Start && other.End <= End)
				return new ValueRange(Start, other.End);

			// If the start of the other range is within this range but the end of the other range
			// is outside (above) this range
			// other: 		|--------|
			// this:	|---------|
			// result:		|-----|
			if (other.End >= End && other.Start >= Start && other.Start <= End)
				return new ValueRange(other.Start, End);

			// Does not intersect
			return null;
		}
	}

	private static List<ValueRange> ParseSeedRanges(List<string> input)
	{
		var seeds = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]
			.Select(x => long.Parse(x))
			.ToList();

		var seedRanges = new List<ValueRange>();
		for(int i = 0; i < seeds.Count; i += 2)
		{
			seedRanges.Add(new ValueRange(seeds[i], seeds[i] + seeds[i + 1] - 1));
		}

		return seedRanges.ToList();
	}

	private static long CalculateLowestLocationFromRanges(List<string> input)
	{
		var mappingsDictionary = ParseMappings(input);
		var seedRanges = ParseSeedRanges(input);

		var sourceRanges = new List<ValueRange>(seedRanges);

		foreach (var mappingDictionary in mappingsDictionary)
		{
			// List of ranges used when going to a new dictionary
			var nextSourceRanges = new List<ValueRange>();

			foreach (var (mapping, index) in mappingDictionary.Value.Select((m, i) => (m, i)))
			{
				// List of ranges used when going to the next mapping in a dictionary
				var nextMappingRanges = new List<ValueRange>();

				foreach (var sourceRange in sourceRanges)
				{
					var overlappingRange = mapping.Range.GetIntersection(sourceRange);

					if (overlappingRange == null)
					{
						// No overlap with with current mapping range, so should be considered for the next mapping range
						nextMappingRanges.Add(sourceRange);
						continue;
					}

					if (mapping.Range.Contains(sourceRange))
					{
						// The source range is contained in the mapping range so it should be considered for the next mapping dictionary
						nextSourceRanges.Add(new ValueRange(sourceRange.Start + mapping.Offset, sourceRange.End + mapping.Offset));
						continue;
					}

					if (sourceRange.Contains(mapping.Range))
					{
						// The mapping range is contained in the source range so it should be considered for the next mapping dictionary
						nextSourceRanges.Add(new ValueRange(mapping.Range.Start + mapping.Offset, mapping.Range.End + mapping.Offset));

						// The outside edges of the source range need to be considered for the next mapping dictionary
						nextMappingRanges.Add(new ValueRange(sourceRange.Start, mapping.Range.Start - 1));	
						nextMappingRanges.Add(new ValueRange(mapping.Range.End + 1, sourceRange.End));
						continue;
					}

					// There was overlap with the ranges but one wasn't fully contained within another so need to add the overlapping range
					// Either:	|-------|
					//				|------|
					// -----------------------------
					// Or:		|------|
					//		|------|
					nextSourceRanges.Add(new ValueRange(overlappingRange.Start + mapping.Offset, overlappingRange.End + mapping.Offset));

					// The opposite non intersection/overlapping edge needs to be added to the next mapping's range ([--] in the below diagrams)
					// Source:	[---]---|
					// Mapping:		|------|
					if (sourceRange.Start < mapping.Range.Start)
						nextMappingRanges.Add(new ValueRange(sourceRange.Start, mapping.Range.Start - 1));
					// Source:		|---[---]
					// Mapping:	|-------|
					else if (sourceRange.End > mapping.Range.End)
						nextMappingRanges.Add(new ValueRange(mapping.Range.End + 1, sourceRange.End));
				}

				// After the last mapping of a dictionary add the compiled list of intersecting ranges to the list 
				// used for the next mapping dictionary
				if (index >= mappingDictionary.Value.Count - 1)
					nextSourceRanges.AddRange(nextMappingRanges);
				// Otherwise transfer the compiled list of intersecting ranges to the list used for the next mapping in
				// the current dictionary
				else
					sourceRanges = nextMappingRanges;
			}
			sourceRanges = nextSourceRanges;
		}

		return sourceRanges.Min(x => x.Start);
	}
	#endregion
}
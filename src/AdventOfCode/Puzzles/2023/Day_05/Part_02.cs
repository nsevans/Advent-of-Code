using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Day_05;

public class Part_02 : Day_05
{
	public override int Part => 2;

	private Dictionary<int, List<Mapping>> _mappingsDictionary;
	private List<ValueRange> _seedRanges;
    
	public override void PrepareData(List<string> input)
    {
		_mappingsDictionary = ParseMappings(input);
		_seedRanges = ParseSeedRanges(input);
    }

	private List<ValueRange> ParseSeedRanges(List<string> input)
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

	public override string ResultMessage => "Lowest location value with seed range";

    public override string GetResult()
    {
		return CalculateLowestLocationFromRanges(_mappingsDictionary, _seedRanges).ToString();
    }

	private long CalculateLowestLocationFromRanges(Dictionary<int, List<Mapping>> mappingsDictionary, List<ValueRange> seedRanges)
	{
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
}
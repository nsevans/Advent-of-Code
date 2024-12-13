using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Day_05;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2023/day/5
/// 
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
public abstract class Day_05 : BaseSolver
{
	public override string Title => "If You Give A Seed A Fertilizer";
    public override int Day => 5;
    public override int Year => 2023;

	protected record Mapping(long RangeStart, long RangeEnd, long Offset)
	{
		public ValueRange Range => new ValueRange(RangeStart, RangeEnd);
	};

	protected record ValueRange(long Start, long End)
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

	protected static Dictionary<int, List<Mapping>> ParseMappings(List<string> input)
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
}
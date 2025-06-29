using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_09;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/9
///
/// Example/Test Input:
/// 	2333133121414131402
/// </summary>
public abstract class Day_09 : BaseSolver
{
	public override string Title => "Disk Fragmenter";
	public override int Day => 9;
	public override int Year => 2024;

	protected static List<int> ParseDiskMap(IEnumerable<string> input)
	{
		return input.First().Select(x => int.Parse(x.ToString())).ToList();
	}

	protected static List<string> ParseExpandedDiskMap(List<int> diskMap)
	{
		var expandedDiskMap = new List<string>();
		var currentId = 0;
		for (var i = 0; i < diskMap.Count; i++)
		{
			// File
			if (i % 2 == 0)
			{
				var file = Enumerable.Repeat(currentId.ToString(), diskMap[i]).ToList();
				expandedDiskMap.AddRange(file);
				currentId++;
			}
			// Free Space
			else
			{
				var freeSpace = Enumerable.Repeat(".", diskMap[i]).ToList();
				expandedDiskMap.AddRange(freeSpace);
			}
		}

		return expandedDiskMap;
	}
}
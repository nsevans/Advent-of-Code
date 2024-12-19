using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_09;

public class Part_01 : Day_09
{
	public override int Part => 1;

	private List<string> _expandedDiskMap;

	public override void PrepareData(List<string> input)
	{
		var diskMap = ParseDiskMap(input);
		_expandedDiskMap = ParseExpandedDiskMap(diskMap);
	}

	public override void Solve()
	{
		var result = CalculateChecksumOfRoughlyDefragmentedDiskMap(_expandedDiskMap);
		Console.WriteLine($"Checksum of rough defragmentation: {result}");
	}

	private static long CalculateChecksumOfRoughlyDefragmentedDiskMap(List<string> expandedDiskMap)
	{
		for (var i = expandedDiskMap.Count - 1; i >= 0; i--)
		{
			if (expandedDiskMap[i] != ".")
			{
				for (var j = 0; j < i; j++)
				{
					if (expandedDiskMap[j] == ".")
					{
						(expandedDiskMap[i], expandedDiskMap[j]) = (expandedDiskMap[j], expandedDiskMap[i]);
						break;
					}
				}
			}
		}

		var checksum = 0L;
		// Ignore empty spaces for check sum because all will be at the end of the list
		foreach (var (file, index) in expandedDiskMap.Where(x => x != ".").Select((f, i) => (f, i)))
		{
			checksum += index * int.Parse(file);
		}

		return checksum;
	}
}
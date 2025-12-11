using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_09;

public class Part_01 : Day_09
{
	public override int Part => 1;

	public override string ResultMessage => "Checksum of rough defragmentation";

	private List<string> _expandedDiskMap;

	public override void PrepareData(List<string> input)
	{
		var diskMap = ParseDiskMap(input);
		_expandedDiskMap = ParseExpandedDiskMap(diskMap);
	}

	public override string GetResult()
	{
		return CalculateChecksumOfRoughlyDefragmentedDiskMap(_expandedDiskMap.ToArray()).ToString();
	}

	private static long CalculateChecksumOfRoughlyDefragmentedDiskMap(string[] expandedDiskMap)
	{
		var lastCheckedSpace = 0;
		for (var i = expandedDiskMap.Length - 1; i >= 0; i--)
		{
			if (expandedDiskMap[i] != ".")
			{
				for (var j = lastCheckedSpace; j < i; j++)
				{
					if (expandedDiskMap[j] == ".")
					{
						(expandedDiskMap[i], expandedDiskMap[j]) = (expandedDiskMap[j], expandedDiskMap[i]);
						lastCheckedSpace = j + 1;
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
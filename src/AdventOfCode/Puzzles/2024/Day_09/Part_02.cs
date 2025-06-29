using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_09;

public class Part_02 : Day_09
{
	public override int Part => 2;

	private List<string> _expandedDiskMap;

	public override void PrepareData(List<string> input)
	{
		var diskMap = ParseDiskMap(input);
		_expandedDiskMap = ParseExpandedDiskMap(diskMap);
	}

	public override string ResultMessage => "Checksum of defragmentation";

	public override string GetResult()
	{
		return CalculateChecksumOfDefragmentedDiskMap(_expandedDiskMap).ToString();
	}

	private static long CalculateChecksumOfDefragmentedDiskMap(List<string> expandedDiskMap)
	{
		// Keep track of the earliest empty location to make future free space scans quicker
		var earliestFreeSpaceIndex = -1;
		// Keep track of the last file location to make future file scans quicker
		var latestFileChunkIndex = -1;

		for (var i = expandedDiskMap.Count - 1; i >= earliestFreeSpaceIndex; i--)
		{
			if (expandedDiskMap[i] != ".")
			{
				var fileId = expandedDiskMap[i];
				// Get each index of the next file
				var fileIndexes = GetFileIndexes(expandedDiskMap, fileId, i, out latestFileChunkIndex);

				var freeSpaceStartIndex = earliestFreeSpaceIndex < 0 ? 0 : earliestFreeSpaceIndex;
				var freeSpaceIndexes = GetFreeSpaceIndexes(expandedDiskMap, freeSpaceStartIndex, fileIndexes.Count, fileIndexes.First(), out earliestFreeSpaceIndex);

				i = latestFileChunkIndex < 0 ? i : latestFileChunkIndex;
				if (freeSpaceIndexes.Count == 0)
				{
					continue;
				}

				// Swap the location of the file indexes with that of the free space indexes
				foreach (var (fileIndex, freeSpaceIndex) in fileIndexes.Zip(freeSpaceIndexes))
				{
					(expandedDiskMap[fileIndex], expandedDiskMap[freeSpaceIndex]) = (expandedDiskMap[freeSpaceIndex], expandedDiskMap[fileIndex]);
				}
			}
		}

		var checksum = 0L;
		foreach (var (file, index) in expandedDiskMap.Select((f, i) => (f, i)))
		{
			if (file == ".")
				continue;
			checksum += index * int.Parse(file);
		}

		return checksum;
	}

	private static List<int> GetFileIndexes(List<string> expandedDiskMap, string fileId, int startIndex, out int latestFileChunkIndex)
	{
		var fileIndexes = new List<int> { };
		var latestFileChunkIndexValue = -1;

		// Only check the next 9 indexes (or until the start of the list) beause the size of
		// a single file can be no larger than 9
		var endIndex = startIndex - 9 < 0 ? 0 : startIndex - 9;

		for (var i = startIndex; i >= endIndex; i--)
		{
			if (expandedDiskMap[i] == fileId)
			{
				fileIndexes.Add(i);
				latestFileChunkIndexValue = i;
			}

			// Found the end of the file
			if (expandedDiskMap[i] != fileId && fileIndexes.Count > 0)
			{
				break;
			}
		}

		latestFileChunkIndex = latestFileChunkIndexValue;
		return fileIndexes.OrderBy(x => x).ToList();
	}

	private static List<int> GetFreeSpaceIndexes(List<string> expandedDiskMap, int startIndex, int fileSize, int firstFileIndex, out int earliestFreeSpaceIndex)
	{

		var freeSpaceIndexes = new List<int>();
		var foundEarliestFreeSpaceIndex = false;
		var earliestFreeSpaceIndexValue = startIndex;

		for (var i = earliestFreeSpaceIndexValue; i < firstFileIndex; i++)
		{
			if (expandedDiskMap[i] == ".")
			{
				freeSpaceIndexes.Add(i);
				if (!foundEarliestFreeSpaceIndex)
				{
					foundEarliestFreeSpaceIndex = true;
					earliestFreeSpaceIndexValue = i;
				}
			}
			else
				freeSpaceIndexes = [];

			if (freeSpaceIndexes.Count == fileSize)
				break;
		}

		earliestFreeSpaceIndex = earliestFreeSpaceIndexValue;

		if (freeSpaceIndexes.Count == fileSize)
			return freeSpaceIndexes.OrderBy(x => x).ToList();

		return [];
	}
}
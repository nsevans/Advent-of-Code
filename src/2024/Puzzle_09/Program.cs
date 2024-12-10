using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_09;

/// <summary>
/// Link: https://adventofcode.com/2024/day/9
/// Input Format:
/// 	2333133121414131402
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 09 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateChecksumOfRoughlyDefragmentedDiskMap(input);
		Console.WriteLine($"[PART 1] Checksum of rough defragmentation: {resultPart1}");

		var resultPart2 = CalculateChecksumOfDefragmentedDiskMap(input);
		Console.WriteLine($"[PART 2] Checksum of defragmentation: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private static List<int> ParseDiskMap(IEnumerable<string> input)
	{
		return input.First().Select(x => int.Parse(x.ToString())).ToList();
	}

	private static List<string> ParseExpandedDiskMap(List<int> diskMap)
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

	#region PART 1
	private static long CalculateChecksumOfRoughlyDefragmentedDiskMap(IEnumerable<string> input)
	{
		var diskMap = ParseDiskMap(input);
		var expandedDiskMap = ParseExpandedDiskMap(diskMap);

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
	#endregion

	#region PART 2
	private static long CalculateChecksumOfDefragmentedDiskMap(IEnumerable<string> input)
	{
		var diskMap = ParseDiskMap(input);
		var expandedDiskMap = ParseExpandedDiskMap(diskMap);

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
				freeSpaceIndexes = new List<int>();

			if (freeSpaceIndexes.Count == fileSize)
				break;
		}

		earliestFreeSpaceIndex = earliestFreeSpaceIndexValue;

		if (freeSpaceIndexes.Count == fileSize)
			return freeSpaceIndexes.OrderBy(x => x).ToList();

		return new List<int>();
	}
	#endregion
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_10;

/// <summary>
/// Link: https://adventofcode.com/2024/day/10
/// Input Format:
/// 	2333133121414131402
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 10 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfDistinctTrailHeadScores(input);
		Console.WriteLine($"[PART 1] Sum of distinct trail heads to trail ends: {resultPart1}");

		var resultPart2 = CalculateSumOfTrailHeadScores(input);
		Console.WriteLine($"[PART 2] Sum of all trail heads to trail ends: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private static List<List<int>> ParseTrailMap(IEnumerable<string> input)
	{
		return input.Select(x => x.Select(x => int.Parse(x.ToString())).ToList()).ToList();
	}

	private static List<(int x, int y)> ParseLocations(List<List<int>> input, int locationId)
	{
		var locations = new List<(int x, int y)>();

		foreach (var (line, y) in input.Select((l, i) => (l, i)))
		{
			foreach (var (value, x) in line.Select((v, i) => (v, i)))
			{
				if (value == locationId)
					locations.Add((x, y));
			}
		}

		return locations;
	}

	 private static List<(int x, int y)> TryFindEndOfTrailRecursiveDFS(List<List<int>> trailMap, (int x, int y) position)
    {
		var topsFound = new List<(int x, int y)>();

		if (trailMap[position.y][position.x] == 9)
			return [ (position.x, position.y) ];

		var currentPositionValue = trailMap[position.y][position.x];

		// Ensure left position is valid and check it if it is
		if (position.x - 1 >= 0 && trailMap[position.y][position.x - 1] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x - 1, position.y)));

		// Ensure right position is valid and check it if it is 
		if (position.x + 1 < trailMap[position.y].Count && trailMap[position.y][position.x + 1] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x + 1, position.y)));

		// Ensure top position is valid and check it if it is
		if (position.y - 1 >= 0 && trailMap[position.y - 1][position.x] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x, position.y - 1)));

		// Ensure bottom position is valid and check it if it is
		if (position.y + 1 < trailMap.Count && trailMap[position.y + 1][position.x] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x, position.y + 1)));

		return topsFound;
    }

	#region PART 1
	private static long CalculateSumOfDistinctTrailHeadScores(IEnumerable<string> input)
	{
		var trailMap = ParseTrailMap(input);
		var startLocations = ParseLocations(trailMap, 0);
		var endPositions = ParseLocations(trailMap, 9);

		var endsFound = new List<(int x, int y)>();

		foreach (var startLocation in startLocations)
		{
			var potentialEndsFound = TryFindEndOfTrailRecursiveDFS(trailMap, startLocation);
			// Ensure each connected trail end (9) is only added once per trail start (0)
			var distinctTrailEndsFound = potentialEndsFound.Distinct().Where(x => endPositions.Contains(x));
			endsFound.AddRange(distinctTrailEndsFound);
		}

		return endsFound.Count;
	}
    #endregion

    #region PART 2
	private static long CalculateSumOfTrailHeadScores(IEnumerable<string> input)
	{
		var trailMap = ParseTrailMap(input);
		var startLocations = ParseLocations(trailMap, 0);
		var endPositions = ParseLocations(trailMap, 9);

		var endsFound = new List<(int x, int y)>();

		foreach (var startLocation in startLocations)
		{
			var potentialEndsFound = TryFindEndOfTrailRecursiveDFS(trailMap, startLocation);
			var trailEndsFound = potentialEndsFound.Where(x => endPositions.Contains(x));
			endsFound.AddRange(trailEndsFound);
		}

		return endsFound.Count;
	}
    #endregion
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Year_2024.Puzzle_08;

/// <summary>
/// Link: https://adventofcode.com/2024/day/8
/// Input Format:
/// 	............
///		........0...
///		.....0......
///		.......0....
///		....0.......
///		......A.....
///		............
///		............
///		........A...
///		.........A..
///		............
///		............
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 08 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfAntinodesForMatchingFrequenciesAtSpecificDistance(input);
		Console.WriteLine($"[PART 1] Sum of unique antinode positions for matching frequencies at specific distance: {resultPart1}");

		var resultPart2 = CalculateSumOfAntinodesForMatchingFrequenciesAtAnyDistance(input);
		Console.WriteLine($"[PART 2] Sum of unique antinode positions for matching frequencies at any distance: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}
	
	private static List<List<char>> ParseInputToMap(IEnumerable<string> input)
	{
		return input.Select(l => l.ToCharArray().ToList()).ToList();
	}

	private static Dictionary<char, List<Vector2>> ParseInputToDictionary(IEnumerable<string> input)
	{
		// Convert input to dictionary of Lists of Vectors to more easily identify matching antenna frequencies
		var dictionary = new Dictionary<char, List<Vector2>>();

		foreach (var (line, yCoord) in input.Select((l, y) => (l, y)))
		{
			foreach (var (point, xCoord) in line.Select((p, x) => (p, x)))
			{
				if (point == '.')
					continue;

				if (dictionary.TryGetValue(point, out var coords))
					coords.Add(new Vector2(xCoord, yCoord));
				else
					dictionary.Add(point, [ new Vector2(xCoord, yCoord) ]);
			}
		}

		return dictionary;
	}

	private static bool IsInBounds(Vector2 coord, List<List<char>> map)
	{
		if (coord.X < 0 || coord.Y < 0)
			return false;

		if (coord.Y >= map.Count || coord.X >= map[(int)coord.Y].Count)
			return false;

		return true;
	}

	#region PART 1
	private static int CalculateSumOfAntinodesForMatchingFrequenciesAtSpecificDistance(IEnumerable<string> input)
	{
		var frequencyDictionary = ParseInputToDictionary(input);
		var map = ParseInputToMap(input);

		var antinodeLocations = new List<Vector2>();

		foreach (var frequency in frequencyDictionary)
		{
			foreach (var antenna1 in frequency.Value)
			{
				foreach (var antenna2 in frequency.Value.Where(v => v != antenna1))
				{
					var direction = new Vector2(antenna2.X - antenna1.X, antenna2.Y - antenna1.Y);

					var antinode1 = antenna1 - direction;
					// Ensure antinode is in bounds and position isn't intersecting with antenna with matching frequency
					if (IsValidAntinodePosition(antinode1, frequency.Key, map))
						antinodeLocations.Add(new Vector2(antinode1.X, antinode1.Y));

					// Ensure antinode is in bounds and position isn't intersecting with antenna with matching frequency
					var antinode2 = antenna2 + direction;
					if (IsValidAntinodePosition(antinode2, frequency.Key, map))
						antinodeLocations.Add(new Vector2(antinode2.X, antinode2.Y));
				}
			}
		}

		return antinodeLocations.Distinct().Count();
	}

	private static bool IsValidAntinodePosition(Vector2 antinode, char frequency, List<List<char>> map)
	{
		return IsInBounds(antinode, map) && map[(int)antinode.Y][(int)antinode.X] != frequency;
	}
	#endregion

	#region PART 2
	private static int CalculateSumOfAntinodesForMatchingFrequenciesAtAnyDistance(IEnumerable<string> input)
	{
		var frequencyDictionary = ParseInputToDictionary(input);
		var map = ParseInputToMap(input);

		var antinodeLocations = new List<Vector2>();

		foreach (var frequency in frequencyDictionary)
		{
			foreach (var antenna1 in frequency.Value)
			{
				foreach (var antenna2 in frequency.Value.Where(v => v != antenna1))
				{
					var direction = new Vector2(antenna2.X - antenna1.X, antenna2.Y - antenna1.Y);

					// Check one direction
					var nextAntinode = antenna1;
					var iterations = 0;
					while (IsInBounds(nextAntinode, map))
					{
						antinodeLocations.Add(new Vector2(nextAntinode.X, nextAntinode.Y));
						iterations++;
						nextAntinode = antenna1 - (iterations * direction);
					}

					// Check the other direction
					nextAntinode = antenna2;
					iterations = 0;
					while (IsInBounds(nextAntinode, map))
					{
						antinodeLocations.Add(new Vector2(nextAntinode.X, nextAntinode.Y));
						iterations++;
						nextAntinode = antenna2 + (iterations * direction);
					}
				}
			}
		}

		return antinodeLocations.Distinct().Count(); 
	}
	#endregion
}
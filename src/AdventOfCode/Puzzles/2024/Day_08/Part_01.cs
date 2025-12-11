using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_08;

public class Part_01 : Day_08
{
	public override int Part => 1;

	public override string ResultMessage => "Sum of unique antinode positions for matching frequencies at specific distance";

	private Dictionary<char, List<Vector2>> _frequencyDictionary;
	private List<List<char>> _map;

	public override void PrepareData(List<string> input)
	{
		_frequencyDictionary = ParseFrequenceyDictionary(input);
		_map = input.To2DCharList();
	}

	public override string GetResult()
	{
		return CalculateSumOfAntinodesForMatchingFrequenciesAtSpecificDistance(_frequencyDictionary, _map).ToString();
	}

	private static int CalculateSumOfAntinodesForMatchingFrequenciesAtSpecificDistance(Dictionary<char, List<Vector2>> frequencyDictionary, List<List<char>> map)
	{
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
		return map.IsInBounds((int) antinode.X, (int) antinode.Y) && map[(int)antinode.Y][(int)antinode.X] != frequency;
	}

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_08;

public class Part_02 : Day_08
{
	public override int Part => 2;

	private Dictionary<char, List<Vector2>> _frequencyDictionary;
	private List<List<char>> _map;

	public override void PrepareData(List<string> input)
	{
		_frequencyDictionary = ParseFrequenceyDictionary(input);
		_map = input.To2DCharList();
	}

	public override string ResultMessage => "Sum of unique antinode positions for matching frequencies at any distance";

	public override string GetResult()
	{
		return CalculateSumOfAntinodesForMatchingFrequenciesAtAnyDistance(_frequencyDictionary, _map).ToString();
	}

	private int CalculateSumOfAntinodesForMatchingFrequenciesAtAnyDistance(Dictionary<char, List<Vector2>> frequencyDictionary, List<List<char>> map)
	{
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
					while (map.IsInBounds((int) nextAntinode.X, (int) nextAntinode.Y))
					{
						antinodeLocations.Add(new Vector2(nextAntinode.X, nextAntinode.Y));
						iterations++;
						nextAntinode = antenna1 - (iterations * direction);
					}

					// Check the other direction
					nextAntinode = antenna2;
					iterations = 0;
					while (map.IsInBounds((int) nextAntinode.X, (int) nextAntinode.Y))
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
}
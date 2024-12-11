using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_11;

/// <summary>
/// Link: https://adventofcode.com/2024/day/11
/// Input Format:
/// 	2333133121414131402
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 11 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateNumberOfStonesAfter25Blinks(input);
		Console.WriteLine($"[PART 1]: Number of stones after 25 blinks: {resultPart1}");
		
		var resultPart2 = CalculateNumberOfStonesAfter75Blinks(input);
		Console.WriteLine($"[PART 2]: Number of stones after 75 blinks: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	public static List<long> ParseInput(IEnumerable<string> input)
	{
		return input
			.SelectMany(x => x
				.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(x => long.Parse(x)))
			.ToList();

	}

	#region PART 1
	private static long CalculateNumberOfStonesAfter25Blinks(IEnumerable<string> input)
	{
		var stones = ParseInput(input);
		// Cache of stone values and what new stone values they convert to
		var cache = new Dictionary<long, List<long>>();

		for (var c = 0; c < 25; c++)
		{
			var newStones = new List<long>();

			for (var i = 0; i < stones.Count; i++)
			{

				if (stones[i] == 0)
				{
					newStones.Add(1L);
				}
				else if (cache.TryGetValue(stones[i], out var result))
				{
					newStones.AddRange(result);
				}
				else if (stones[i].ToString().Length % 2 == 0)
				{
					var asString = stones[i].ToString();

					var firstHalf = long.Parse(asString[..(asString.Length / 2)]);
					newStones.Add(firstHalf);

					var secondHalf = long.Parse(asString[(asString.Length / 2)..]);
					newStones.Add(secondHalf);

					cache.Add(stones[i], [ firstHalf, secondHalf ]);
				}
				else
				{
					var newResult = stones[i] * 2024L;
					newStones.Add(newResult);
					cache.Add(stones[i], [ newResult ]);
				}
			}
			stones = newStones;
		}

		return stones.Count;
	}
	#endregion

	#region PART 2
	private static long CalculateNumberOfStonesAfter75Blinks(IEnumerable<string> input)
	{
		var stones = ParseInput(input);
		// Cache of stone value and blink number and what stone value they convert to
		var cache = new Dictionary<(long, int), long>();
		var count = 0L;

		foreach(var stone in stones)
		{
			count += CountStonesRecursive(stone, cache, 75);
		}

		return count;
	}

	private static long CountStonesRecursive(long stone, Dictionary<(long, int), long> cache, int iterationsRemaining)
	{
		if (cache.TryGetValue((stone, iterationsRemaining), out var cachedCount))
			return cachedCount;

		var count = 0L;

		if (iterationsRemaining == 0)
			count = 1L;


		else if (stone == 0)
		{
			count += CountStonesRecursive(1, cache, iterationsRemaining - 1);
		}
		else if ((int) Math.Floor(Math.Log10(stone) + 1) is var numberLength && numberLength % 2 == 0)
		{
			var divisor = Math.Pow(10, numberLength / 2);

			var firstStone = (long) (stone / divisor);
			var secondStone = (long) (stone % divisor);

			count += CountStonesRecursive(firstStone, cache, iterationsRemaining - 1) + CountStonesRecursive(secondStone, cache, iterationsRemaining - 1);
		}
		else
		{
			count += CountStonesRecursive(stone * 2024L, cache, iterationsRemaining - 1);
		}

		cache.Add((stone, iterationsRemaining), count);

		return count;
	}
	#endregion
}
using System;
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_11;

public class Part_02 : Day_11
{
	public override int Part => 2;

	private List<long> _stones;

	public override void PrepareData(List<string> input)
	{
		_stones = ParseStones(input);
	}

	public override void Solve()
	{
		var result = CalculateNumberOfStonesAfter75Blinks(_stones);
		Console.WriteLine($"Number of stones after 75 blinks: {result}");
	}

	private long CalculateNumberOfStonesAfter75Blinks(List<long> stones)
	{
		// Cache of stone value and blink number and what stone value they convert to
		var cache = new Dictionary<(long, int), long>();
		var count = 0L;

		foreach(var stone in stones)
		{
			count += CountStonesRecursive(stone, cache, 75);
		}

		return count;
	}

	private long CountStonesRecursive(long stone, Dictionary<(long, int), long> cache, int iterationsRemaining)
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
}
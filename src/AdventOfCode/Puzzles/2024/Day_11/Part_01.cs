using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_11;

public class Part_01 : Day_11
{
	public override int Part => 1;

	private List<long> _stones;

	public override void PrepareData(List<string> input)
	{
		_stones = ParseStones(input);
	}

	public override string ResultMessage => "Number of stones after 25 blinks";

	public override string GetResult()
	{
		return CalculateNumberOfStonesAfter25Blinks(_stones).ToString();
	}

	private static long CalculateNumberOfStonesAfter25Blinks(List<long> stones)
	{
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
}
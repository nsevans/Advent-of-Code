using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_25;

public class Part_01 : Day_25
{
    public override int Part => 1;

    public override string ResultMessage => "Number of unique key/lock combinations";

	private List<List<int>> _locks;
	private List<List<int>> _keys;

	public override void PrepareData(List<string> input)
	{
		_locks = ParseLocks(input);
		_keys = ParseKeys(input);
	}

	private static List<List<int>> ParseLocks(List<string> input)
	{
		var locks = new List<List<int>>();

		for (var i = 0; i < input.Count; i += 8)
		{
			// First line of input chunk starts with '.', then it's a key, not a lock
			if (input[i].StartsWith('.'))
				continue;

			var lockDiagram = input[i..(i+7)].To2DCharList();
			var lockHeights = new List<int>();
			for (var j = 0; j < lockDiagram[0].Count; j++)
			{
				var height = 0;
				for (var k = 0; k < lockDiagram.Count; k++)
				{
					if (lockDiagram[k][j] == '#')
						height++;
					else
						break;
				}
				lockHeights.Add(height - 1);
			}

			locks.Add(lockHeights);
		}
		return locks;
	}

	private static List<List<int>> ParseKeys(List<string> input)
	{
		var locks = new List<List<int>>();

		for (var i = 0; i < input.Count; i += 8)
		{
			// First line of input chunk starts with '.', then it's a key, not a lock
			if (!input[i].StartsWith('.'))
				continue;

			var keyDiagram = input[i..(i+7)].To2DCharList();
			var keyHeights = new List<int>();
			for (var j = 0; j < keyDiagram[0].Count; j++)
			{
				var height = 0;
				for (var k = 0; k < keyDiagram.Count; k++)
				{
					if (keyDiagram[k][j] == '#')
						height++;
				}
				keyHeights.Add(height - 1);
			}

			locks.Add(keyHeights);
		}
		return locks;
	}

	public override string GetResult()
	{
		return SumOfUniqueKeyCombinations(_locks, _keys).ToString();
	}

	private static int SumOfUniqueKeyCombinations(List<List<int>> locks, List<List<int>> keys)
	{
		var uniqueCombos = 0;

		foreach (var key in keys)
		{
			// Iterate over each lock, but reduce size of set by ensuring the first item in each array sums to 5 (they fit together)
			foreach (var @lock in locks.Where(l => l[0] + key[0] <= 5))
			{
				var keyFits = true;
				for (var i = 0; i < key.Count; i++)
				{
					if (key[i] + @lock[i] > 5)
					{
						keyFits = false;
						break;
					}
				}

				if (keyFits)
					uniqueCombos++;
			}
		}

		return uniqueCombos;
	}
}
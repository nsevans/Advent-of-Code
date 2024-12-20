using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_01;

public class Part_01 : Day_01
{
	public override int Part => 1;

	private (List<long> leftItems, List<long> rightItems) _inputPairs;

	public override void PrepareData(List<string> input)
	{
		_inputPairs = ParsePairs(input);
	}

	public override void Solve()
	{
		var result = CalculateSumOfDifferencesOfValues(_inputPairs.leftItems, _inputPairs.rightItems);
		Console.WriteLine($"Sum of differences of ordered distances: {result}");
	}

	private long CalculateSumOfDifferencesOfValues(List<long> left, List<long> right)
    {
		var sum = 0L;
		left.Sort();
		right.Sort();

		foreach(var (leftItem, rightItem) in left.Zip(right))
		{
			sum += Math.Abs(leftItem - rightItem);
		}

		return sum;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_01;

public class Part_02 : Day_01
{
	public override int Part => 2;

	private (List<long> leftItems, List<long> rightItems) _inputPairs;

	public override void PrepareData(List<string> input)
	{
		_inputPairs = ParsePairs(input);
	}

	public override string ResultMessage => "Sum of product of duplicate values";

	public override string GetResult()
	{
		return CalculateSumOfProductOfDuplicateValues(_inputPairs.leftItems, _inputPairs.rightItems).ToString();
	}

	private static long CalculateSumOfProductOfDuplicateValues(List<long> left, List<long> right)
	{
		var sum = 0L;

		foreach(var leftItem in left)
		{
			sum += leftItem * right.Count(x => x == leftItem);
		}

		return sum;
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_02;

public class Part_01 : Day_02
{
	public override int Part => 1;

    public override string ResultMessage => "Sum of all safe files (lines with gradual increases or decreases)";

	private List<string> _preparedInput;

    public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

	public override string GetResult()
    {
		return CalculateSumOfSafelyIncreasingOrDecreasingLines(_preparedInput).ToString();
    }

	private static int CalculateSumOfSafelyIncreasingOrDecreasingLines(IEnumerable<string> input)
	{
		var sum = 0;
		foreach(var line in input)
		{
			var lineNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
			var isAscending = lineNumbers[0] < lineNumbers[1];
			var isValidLine = true;

			for (int i = 0; i < lineNumbers.Count - 1; i++)
			{
				var currentNumber = lineNumbers[i];
				var nextNumber = lineNumbers[i + 1];

				if (!ValidateSet(currentNumber, nextNumber, isAscending))
				{
					isValidLine = false;
					break;
				}
			}

			if (isValidLine)
				sum++;
		}

		return sum;
	}
}

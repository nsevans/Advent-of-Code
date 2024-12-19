using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_02;

public class Part_02 : Day_02
{
	public override int Part => 2;

	private List<string> _preparedInput;
	
    public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

    public override void Solve()
    {
		var result = CalculateSumOfSafelyIncreasingOrDecreasingLinesWithDampener(_preparedInput);
		Console.WriteLine($"Sum of all safe files (lines with gradual increases or decreases) with tolerance: {result}");
    }

	private int CalculateSumOfSafelyIncreasingOrDecreasingLinesWithDampener(IEnumerable<string> input)
	{
		var sum = 0;
		foreach(var line in input)
		{
			var lineNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
			var isValidLine = ValidateLineWithTolerance(lineNumbers);

			if (isValidLine)
				sum++;
		}
		return sum;
	}

	private bool ValidateLineWithTolerance(List<int> lineNumbers)
	{
		// Try each combination of the line removing a different index each time
		for (var i = 0; i < lineNumbers.Count; i ++)
		{
			var copy = new List<int>(lineNumbers);
			copy.RemoveAt(i);

			var isAscending = copy[0] < copy[1];
			var isValidLine = true;
			
			// Iterate over the new list with the removed index to see if it's valid
			for (var j = 0; j < copy.Count - 1; j++)
			{
				var currentNumber = copy[j];
				var nextNumber = copy[j + 1];

				// If an invalid set is found break and try the next line combination
				if (!ValidateSet(currentNumber, nextNumber, isAscending))
				{
					isValidLine = false;
					break;
				}
			}

			// If a valid combination is found, return true asap
			if (isValidLine)
				return true;
		}

		return false;
	}
}
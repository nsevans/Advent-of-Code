using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Puzzle_01;

public class Part_02 : Puzzle_01
{
	private List<string> _preparedInput;
    public override void PrepareData(List<string> input)
    {
        _preparedInput = input;
    }

    public override void Solve()
    {
		var result = CalculateSumOfCalibrationDigitsAndStrings(_preparedInput);
		Console.WriteLine($"Sum of combined first and last digit or string number of each line: {result}");
    }

	private int CalculateSumOfCalibrationDigitsAndStrings(IEnumerable<string> input)
	{
		var result = 0;
		foreach (var line in input)
		{
			result += ScanForDigitsAndStrings(line);
		}

		return result;
	}

	public int ScanForDigitsAndStrings(string line)
	{
		// First character of each number from 1 to 9
		var startNumberChars = NumberStrings.Select(x => x.First()).Distinct();

		var first = 0;
		for (int i = 0; i < line.Length; i++)
		{
			// If it's a digit
			if (int.TryParse(line[i].ToString(), out var firstItem))
			{
				first = firstItem;
				break;
			}

			// If it's a possible number character
			if (startNumberChars.Contains(line[i]))
			{
				if (TryGetNumberFromString(line, i, out var parsedNumber))
				{
					first = parsedNumber;
					break;
				}
			}
		}

		var last = -1;
		for (int i = line.Length - 1; i >= 0; i--)
		{
			// If it's a digit
			if (int.TryParse(line[i].ToString(), out var lastItem))
			{
				last = lastItem;
				break;
			}

			// If it's a possible number character
			if (startNumberChars.Contains(line[i]))
			{
				if (TryGetNumberFromString(line, i, out var parsedNumber))
				{
					last = parsedNumber;
					break;
				}
			}
		}

		return int.Parse($"{first}{last}");
	}

	private bool TryGetNumberFromString(string line, int startIndex, out int number)
	{
		// Potential candidates this number string could be, based on the letter it starts with
		// This will reduce the amount of numbers that need to be checked for each iteration
		var candidates = NumberStrings.Where(x => x.StartsWith(line[startIndex]));

		// String to compile possible number string
		var numberString = line[startIndex].ToString();

		// Compile string until a match is found
		var matchFound = false;
		for (int j = startIndex + 1; j < line.Length; j++)
		{
			numberString += line[j];
			if (candidates.Contains(numberString))
			{
				matchFound = true;
				break;
			}
		}

		if (matchFound)
		{
			// Convert string to integer
			number = NumberStrings.IndexOf(numberString) + 1;
			return true;
		}

		number = -1;
		return false;
	}
}
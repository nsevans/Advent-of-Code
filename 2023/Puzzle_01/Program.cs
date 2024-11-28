using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzle_01;

/// <summary>
/// Link: https://adventofcode.com/2023/day/1
/// Input Format: mrjstg5onetwoeightgcczx8vgrgl
/// </summary>

public class Program
{
	private static List<string> _numberStrings => ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

	public static void Main(string[] args)
	{
		Console.WriteLine("### 2023 - Puzzle 01 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfCalibrationDigits(input);
		Console.WriteLine($"[PART 1] Sum of combined first and last digit of each line: {resultPart1}");

		var resultPart2 = CalculateSumOfCalibrationDigitsAndStrings(input);
		Console.WriteLine($"[PART 2] Sum of combined first and last digit or string number of each line: {resultPart2}");
	}

	public static IEnumerable<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName);
	}

	public static int CalculateSumOfCalibrationDigits(IEnumerable<string> input)
	{
		var result = 0;
		foreach (var line in input)
		{
			result += ScanForDigits(line);
		}

		return result;
	}

	public static int CalculateSumOfCalibrationDigitsAndStrings(IEnumerable<string> input)
	{
		var result = 0;
		foreach (var line in input)
		{
			result += ScanForDigitsAndStrings(line);
		}

		return result;
	}

	public static int ScanForDigits(string line)
	{
		var first = 0;
		// Iterate forward through the string until a number is found
		for (int i = 0; i < line.Length; i++)
		{
			// If it's a number, set it and continue
			if (int.TryParse(line[i].ToString(), out var firstItem))
			{
				first = firstItem;
				break;
			}
		}

		var last = 0;
		// Iterate backward through the string until a number is found
		for (int i = line.Length - 1; i >= 0; i--)
		{
			// If it's a number, set it and continue
			if (int.TryParse(line[i].ToString(), out var lastItem))
			{
				last = lastItem;
				break;
			}
		}

		return int.Parse($"{first}{last}");
	}

	public static int ScanForDigitsAndStrings(string line)
	{
		// First character of each number from 1 to 9
		var startNumberChars = _numberStrings.Select(x => x.First()).Distinct();

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

	private static bool TryGetNumberFromString(string line, int startIndex, out int number)
	{
		// Potential candidates this number string could be, based on the letter it starts with
		// This will reduce the amount of numbers that need to be checked for each iteration
		var candidates = _numberStrings.Where(x => x.StartsWith(line[startIndex]));

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
			number = _numberStrings.IndexOf(numberString) + 1;
			return true;
		}

		number = -1;
		return false;
	}
}
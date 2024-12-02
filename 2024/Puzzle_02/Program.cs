using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzle_05;

/// <summary>
/// Link: https://adventofcode.com/2024/day/2
/// Input Format:
/// 	7 6 4 2 1
/// 	1 2 7 8 9
/// 	9 7 6 2 1
/// 	1 3 2 4 5
/// 	8 6 4 4 1
/// 	1 3 6 7 9
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 02 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfSafelyIncreasingOrDecreasingLines(input);
		Console.WriteLine($"[PART 1] Sum of all safe files (lines with gradual increases or decreases): {resultPart1}");

		var resultPart2 = CalculateSumOfSafelyIncreasingOrDecreasingLinesWithDampener(input);
		Console.WriteLine($"[PART 2] Sum of all safe files (lines with gradual increases or decreases) with tolerance: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private static bool ValidateSet(int number1, int number2, bool isAscending)
	{
		var difference = Math.Abs(number1 - number2);
		if (number1 == number2)
			return false;

		else if(isAscending && number1 > number2)
			return false;

		else if(!isAscending && number1 < number2)
			return false;

		else if(difference < 1 || difference > 3)
			return false;

		return true;
	}

	#region PART 1
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
	#endregion

	#region PART 2
	private static int CalculateSumOfSafelyIncreasingOrDecreasingLinesWithDampener(IEnumerable<string> input)
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

	private static bool ValidateLineWithTolerance(List<int> lineNumbers)
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
	#endregion
}
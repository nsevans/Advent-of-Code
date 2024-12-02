using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzle_01;

/// <summary>
/// Link: https://adventofcode.com/2024/day/1
/// Input Format:
/// 	3   4
/// 	4   3
/// 	2   5
/// 	1   3
/// 	3   9
/// 	3   3
/// </summary>

public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 01 ###\n");
		var input = GetInput("./input.txt");

		var parsedInput = ParseInput(input);

		var resultPart1 = CalculateSumOfDifferencesOfValues(parsedInput.Item1, parsedInput.Item2);
		Console.WriteLine($"[PART 1] Sum of differences of ordered distances: {resultPart1}");

		var resultPart2 = CalculateSumOfProductOfDuplicateValues(parsedInput.Item1, parsedInput.Item2);
		Console.WriteLine($"[PART 2] Sum of product of duplicate values: {resultPart2}");
	}

    public static IEnumerable<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName);
	}

	private static (List<long>, List<long>) ParseInput(IEnumerable<string> input)
	{
		var left = new List<long>();
		var right = new List<long>();

		foreach(var line in input)
		{
			var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			left.Add(long.Parse(numbers[0]));
			right.Add(long.Parse(numbers[1]));
		}

		return (left, right);
	}

	#region PART 1
	private static long CalculateSumOfDifferencesOfValues(List<long> left, List<long> right)
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
	#endregion

	#region PART 2
	private static long CalculateSumOfProductOfDuplicateValues(List<long> left, List<long> right)
	{
		var sum = 0L;

		foreach(var leftItem in left)
		{
			sum += leftItem * right.Count(x => x == leftItem);
		}

		return sum;
	}
	#endregion
}
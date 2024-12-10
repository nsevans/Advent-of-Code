using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_07;

/// <summary>
/// Link: https://adventofcode.com/2024/day/7
/// Input Format:
/// 	190: 10 19
///		3267: 81 40 27
///		83: 17 5
///		156: 15 6
///		7290: 6 8 6 15
///		161011: 16 10 13
///		192: 17 8 14
///		21037: 9 7 18 13
///		292: 11 6 16 20
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 07 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfValidEquationResultsWithTwoOperators(input);
		Console.WriteLine($"[PART 1] Sum of valid equations using + and *: {resultPart1}");

		var resultPart2 = CalculateSumOfValidEquationResultsWithThreeOperators(input);
		Console.WriteLine($"[PART 2] SUM of valid equations using +, *, and || (concatenate): {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	public enum Operator
	{
		Add, Multiply, Concatenate
	}

	private static Dictionary<long, List<List<long>>> ParseInput(IEnumerable<string> input)
	{
		var dictionary = new Dictionary<long, List<List<long>>>();

		foreach (var line in input)
		{
			var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
			var key = long.Parse(split[0]);
			var numbers = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList();

			if (dictionary.TryGetValue(key, out var value))
				value.Add(numbers);
			else
				dictionary.Add(key, [ numbers ]);
		}

		return dictionary;
	}

	private static long TryNextOperatorRecursive(long expectedResult, long currentResult, List<long> currentNumbers, Operator op, bool useContactenation = false)
	{
		// If no more numbers or the current result is already greater than the expected result, quit early
		if (currentNumbers.Count == 0 || currentResult > expectedResult)
			return currentResult;

		if (op == Operator.Add)
			currentResult += currentNumbers[0];
		else if (op == Operator.Multiply)
			currentResult *= currentNumbers[0];
		else
			currentResult = long.Parse(currentResult.ToString() + "" + currentNumbers[0]);

		var result1 = TryNextOperatorRecursive(expectedResult, currentResult, currentNumbers[1..], Operator.Add, useContactenation);

		if (result1 == expectedResult)
			return result1;

		var result2 = TryNextOperatorRecursive(expectedResult, currentResult, currentNumbers[1..], Operator.Multiply, useContactenation);
		
		if (result2 == expectedResult || !useContactenation)
			return result2;

		var result3 = TryNextOperatorRecursive(expectedResult, currentResult, currentNumbers[1..], Operator.Concatenate, useContactenation);

		return result3;
	}

	#region PART 1
	private static long CalculateSumOfValidEquationResultsWithTwoOperators(IEnumerable<string> input)
	{
		var sum = 0L;

		var equationDictionary = ParseInput(input);

		foreach (var equation in equationDictionary)
		{
			var expectedResult = equation.Key;
			foreach (var equationNumbers in equation.Value)
			{
				if (CanFindValidOperatorCombination(expectedResult, equationNumbers))
					sum += expectedResult;
			}
		}

		return sum;
	}

	private static bool CanFindValidOperatorCombination(long expectedResult, List<long> numbers)
	{
		var result1 = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Add);
		var result2 = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Multiply);

		if (result1 == expectedResult || result2 == expectedResult)
			return true;

		return false;
	}
	#endregion

	#region PART 2
	private static long CalculateSumOfValidEquationResultsWithThreeOperators(IEnumerable<string> input)
	{
		var sum = 0L;

		var equationDictionary = ParseInput(input);
		foreach (var equation in equationDictionary)
		{
			var expectedResult = equation.Key;
			foreach (var equationNumbers in equation.Value)
			{
				if (CanFindValidOperatorCombinations(expectedResult, equationNumbers))
					sum += expectedResult;
			}
		}

		return sum;
	}

	private static bool CanFindValidOperatorCombinations(long expectedResult, List<long> numbers)
	{
		var result1 = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Add, true);
		var result2 = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Multiply, true);
		var result3 = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Concatenate, true);

		if (result1 == expectedResult || result2 == expectedResult || result3 == expectedResult)
			return true;

		return false;
	}
	#endregion
}
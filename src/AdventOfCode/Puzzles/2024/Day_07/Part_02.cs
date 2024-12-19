using System;
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_07;

public class Part_02 : Day_07
{
	public override int Part => 2;

	private Dictionary<long, List<List<long>>> _equationDictionary;

	public override void PrepareData(List<string> input)
	{
		_equationDictionary = ParseEquationDictionary(input);
	}

	public override void Solve()
	{
		var result = CalculateSumOfValidEquationResultsWithThreeOperators(_equationDictionary);
		Console.WriteLine($"SUM of valid equations using +, *, and || (concatenate): {result}");
	}

	private static long CalculateSumOfValidEquationResultsWithThreeOperators(Dictionary<long, List<List<long>>> equationDictionary)
	{
		var sum = 0L;

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
		var addResult = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Add);
		var multResult = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Multiply);
		var concatResult = TryNextOperatorRecursive(expectedResult, numbers[0], numbers[1..], Operator.Concatenate);

		return addResult == expectedResult || multResult == expectedResult || concatResult == expectedResult;
	}

	private static long TryNextOperatorRecursive(long expectedResult, long currentResult, List<long> currentNumbers, Operator op)
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

		var addResult = TryNextOperatorRecursive(expectedResult, currentResult, currentNumbers[1..], Operator.Add);

		if (addResult == expectedResult)
			return addResult;

		var multResult = TryNextOperatorRecursive(expectedResult, currentResult, currentNumbers[1..], Operator.Multiply);
		
		if (multResult == expectedResult)
			return multResult;

		var concatResult = TryNextOperatorRecursive(expectedResult, currentResult, currentNumbers[1..], Operator.Concatenate);

		return concatResult;
	}
}
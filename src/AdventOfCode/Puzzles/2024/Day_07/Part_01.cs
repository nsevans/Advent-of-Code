using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_07;

public class Part_01 : Day_07
{
	public override int Part => 1;

	public override string ResultMessage => "Sum of valid equations using + and *";

	private Dictionary<long, List<List<long>>> _equationDictionary;

	public override void PrepareData(List<string> input)
	{
		_equationDictionary = ParseEquationDictionary(input);
	}

	public override string GetResult()
	{
		return CalculateSumOfValidEquationResultsWithTwoOperators(_equationDictionary).ToString();
	}

	private static long CalculateSumOfValidEquationResultsWithTwoOperators(Dictionary<long, List<List<long>>> equationDictionary)
	{
		var sum = 0L;
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

		return multResult;
	}
}
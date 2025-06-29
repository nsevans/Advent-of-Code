using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Puzzles.Year_2024.Day_07;

public class Part_02 : Day_07
{
	public override int Part => 2;

	private Dictionary<long, List<List<long>>> _equationDictionary;

	public override void PrepareData(List<string> input)
	{
		_equationDictionary = ParseEquationDictionary(input);
	}

	public override string ResultMessage => "Sum of valid equations using +, *, and || (concatenate)";

	public override string GetResult()
	{
		return CalculateSumOfValidEquationResultsWithThreeOperators(_equationDictionary).ToString();
	}

	private static long CalculateSumOfValidEquationResultsWithThreeOperators(Dictionary<long, List<List<long>>> equationDictionary)
	{
		var sum = 0L;

		var results = new List<long>();
		var currentTasks = new List<Task<long>>();

		var currentIndex = 0;
		var keys = equationDictionary.Keys.ToImmutableArray();

		while (currentIndex < keys.Length)
		{
			var expectedResult = keys[currentIndex];
			var equations = equationDictionary[keys[currentIndex]];

			foreach (var equationNumbers in equations)
			{
				currentTasks.Add(Task.Run(() => CanFindValidOperatorCombinations(expectedResult, equationNumbers)));
			}

			currentIndex++;
		}

		Task.WhenAll(currentTasks);
		sum += currentTasks.Select(t => t.Result).Sum();

		return sum;
	}

	private static long CanFindValidOperatorCombinations(long expectedResult, List<long> numbers)
	{
		var currentResult = numbers[0];
		var currentNumbers = numbers[1..];

		var tasks = new Task<long>[3]
		{
			Task.Run(() => TryNextOperatorRecursiveAsync(expectedResult, currentResult, currentNumbers, Operator.Add)),
			Task.Run(() => TryNextOperatorRecursiveAsync(expectedResult, currentResult, currentNumbers, Operator.Multiply)),
			Task.Run(() => TryNextOperatorRecursiveAsync(expectedResult, currentResult, currentNumbers, Operator.Concatenate)),
		};

		var result = tasks.Select(t => t.Result).Any(r => r == expectedResult);
		return result ? expectedResult : 0L;
	}

	private static long TryNextOperatorRecursiveAsync(long expectedResult, long currentResult, List<long> currentNumbers, Operator op)
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

		var addResult = TryNextOperatorRecursiveAsync(expectedResult, currentResult, currentNumbers[1..], Operator.Add);

		if (addResult == expectedResult)
			return addResult;

		var multResult = TryNextOperatorRecursiveAsync(expectedResult, currentResult, currentNumbers[1..], Operator.Multiply);

		if (multResult == expectedResult)
			return multResult;

		var concatResult = TryNextOperatorRecursiveAsync(expectedResult, currentResult, currentNumbers[1..], Operator.Concatenate);

		return concatResult;
	}
}
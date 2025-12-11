using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Year_2024.Day_03;

public class Part_02 : Day_03
{
	public override int Part => 2;

    public override string ResultMessage => "Sum of products of allowed mul commands";

	private List<string> _preparedInput;

    public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

	public override string GetResult()
    {
		return CalculateSumOfProductsWithCondition(_preparedInput).ToString();
    }

	private static long CalculateSumOfProductsWithCondition(IEnumerable<string> input)
	{
		var sum = 0L;

		// Pattern to find occurences of "mul(xxx,xxx)"
		var mulPattern = @"^mul\((\d{1,3},\d{1,3})\)";
		// Pattern to find occurences of "do()" or "don't()"
		var doOrDontPattern = @"do\(\)|don\'t\(\)";

		var inputLine = string.Join("", input);
		var canMultiply = true;

		for (var i = 0; i < inputLine.Length; i++)
		{
			// Potential "do()" or "don't()"
			if (inputLine[i] == 'd')
			{
				// Get substring length based on distance from current index to end of string
				// Max length of substring is 7 because "don't()" is 7 characters long
				var substringLength = inputLine.Length - i < 7 ? inputLine.Length - i : 7;
				var doOrDontSubstring = inputLine.Substring(i, substringLength);
				var match = Regex.Match(doOrDontSubstring, doOrDontPattern);

				canMultiply = match.Success && match.Value == "do()";
			}
			// Potential "mul(xxx,xxx)", but only consider this option if "do()" was the most
			// recent command out of "do()" or "don't()"
			else if (inputLine[i] == 'm' && canMultiply)
			{
				// Get substring length based on distance from current index to end of string
				// Max length of substring is 12 because "mul(xxx,xxx)" is 12 characters long
				var substringLength = inputLine.Length - i < 12 ? inputLine.Length - i : 12;
				var mulSubstring = inputLine.Substring(i, substringLength);
				var match = Regex.Match(mulSubstring, mulPattern);

				if (match.Success)
					sum += GetProductOfValuesFromMatch(match.Value);
			}
		}
		return sum;
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Puzzle_03;

/// <summary>
/// Link: https://adventofcode.com/2024/day/3
/// Input Format:
/// 	xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
/// </summary>
public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 03 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfProducts(input);
		Console.WriteLine($"[PART 1] Sum of products of mul commands: {resultPart1}");

		var resultPart2 = CalculateSumOfProductsWithCondition(input);
		Console.WriteLine($"[PART 2] Sum of products of allowed mul commands: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private static long GetProductOfValuesFromMatch(string match)
	{
		// Strip out all non digit characters from "mul(xxx,xxx)"
		var values = match.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
		return values[0] * values[1];
	}

	#region PART 1
	private static long CalculateSumOfProducts(IEnumerable<string> input)
	{
		var sum = 0L;
		// Pattern to find occurences of "mul(xxx,xxx)"
		var pattern = @"mul\((\d{1,3},\d{1,3})\)";

		foreach (var line in input)
		{
			var matches = Regex.Matches(line, pattern);

			foreach (var match in matches.Select(x => x.Value))
				sum += GetProductOfValuesFromMatch(match);
		}

		return sum;
	}
	#endregion

	#region PART 2
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
	#endregion
}
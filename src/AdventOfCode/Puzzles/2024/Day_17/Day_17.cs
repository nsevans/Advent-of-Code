using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_17;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/17
/// 
/// Example/Test Input: 
///		Register A: 729
///		Register B: 0
///		Register C: 0
///		
///		Program: 0,1,5,4,3,0
/// </summary>
public abstract class Day_17 : BaseSolver
{
	public override string Title => "Chronospatial Computer";
	public override int Day => 17;
	public override int Year => 2024;

	protected static List<int> ParseProgram(List<string> input)
	{
		var program = new List<int>();

		foreach (var line in input)
		{
			if (!line.StartsWith("Program"))
				continue;

			var values = line.Split(' ')[1]
				.Split(',')
				.Select(int.Parse);

			program.AddRange(values);
			break;
		}

		return program;
	}

	protected static int GetComboOperand(int operand, Dictionary<char, int> registers)
	{
		return operand switch
		{
			0 or 1 or 2 or 3 => operand,
			4 => registers['A'],
			5 => registers['B'],
			6 => registers['C'],
			_ => throw new ArgumentException($"{nameof(operand)} cannot be {operand}, must be between values of 0 and 6 inclusive.")
		};
	}
}
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_17;

public class Part_02 : Day_17
{
	public override int Part => 2;

	private List<int> _program;

	public override void PrepareData(List<string> input)
	{
		// Do not need to parse registers from input as they are not needed for this question
		// Trying to figure out A, and B & C always start at 0
		_program = ParseProgram(input);
	}

	public override string ResultMessage => "The value of register A that makes the programs output equal to itself";

	public override string GetResult()
	{
		return FindRegisterAValue(_program, 0).ToString();
	}

	private static long FindRegisterAValue(List<int> program, long value)
	{
		if (program.Count == 0)
			return value;

		for (long t = 0; t < 8; t++)
		{
			var (a, b, _) = GetRegistryFromProgramRun(value, t);

			if (b % 8 == program[^1])
			{
				var nextValue = FindRegisterAValue(program[..^1], a);
				if (nextValue == -1)
					continue;
				return nextValue;
			}
		}
		return -1;
	}

	/*
	 * TODO: Update to be more general and get result of any program
	 * Would probably require passing in program and running each instruction
	*/
	private static (long a, long b, long c) GetRegistryFromProgramRun(long value, long t)
	{
		long a = value << 3 | t;
		long b = (int)(a % 8);
		b ^= 5;
		long c = a >> (int)b;
		b ^= 6;
		b ^= c;

		return (a, b, c);
	}
}
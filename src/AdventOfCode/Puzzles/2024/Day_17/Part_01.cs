using System;
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_17;

public class Part_01 : Day_17
{
	public override int Part => 1;

	public override string ResultMessage => "The output of the program";

	private Dictionary<char, int> _registers;

	private List<int> _program;

	public override void PrepareData(List<string> input)
	{
		_registers = ParseRegisters(input);
		_program = ParseProgram(input);
	}

	private static Dictionary<char, int> ParseRegisters(List<string> input)
	{
		var registers = new Dictionary<char, int>();

		foreach (var line in input)
		{
			if (!line.StartsWith("Register"))
				break;

			var key = line.Split(' ')[1].ToUpper()[0];
			var value = int.Parse(line.Split(' ')[2]);

			registers.Add(key, value);
		}

		return registers;
	}

	public override string GetResult()
	{
		return GetProgramOutput(_program, _registers);
	}

	private static string GetProgramOutput(List<int> program, Dictionary<char, int> registers)
	{
		var pointer = 0;
		var output = Array.Empty<int>();

		while (pointer < program.Count)
		{
			var instruction = program[pointer];
			var operand = program[pointer + 1];

			switch (instruction)
			{
				// (adv) Integer Division: A = A // 2 ^ combo operand
				case 0:
					registers['A'] = registers['A'] >> GetComboOperand(operand, registers);
					break;
				// (bxl) Bitwise XOR: B = B ^ 1
				case 1:
					registers['B'] = registers['B'] ^ operand;
					break;
				// (bst) Modulo: B = combo operand % 8
				case 2:
					registers['B'] = GetComboOperand(operand, registers) % 8;
					break;
				// (jnz) Jump: Pointer = Operand, Move pointer to operand value only if register A is not 0
				case 3:
					if (registers['A'] != 0)
					{
						pointer = operand;
						// Don't want the pointer to jump ahead by 2, so continue to next iteration of loop
						continue;
					}
					break;
				// (bxc) Bitwise XOR: B = B ^ C
				case 4:
					registers['B'] = registers['B'] ^ registers['C'];
					break;
				// (out) Output: combo operand % 8
				case 5:
					output = [.. output, GetComboOperand(operand, registers) % 8];
					break;
				// (bdv) Integer Division: B = A // 2 ^ combo operand
				case 6:
					registers['B'] = registers['A'] >> GetComboOperand(operand, registers);
					break;
				// (cdv) Integer Division: C = A // 2 ^ combo operand
				case 7:
					registers['C'] = registers['A'] >> GetComboOperand(operand, registers);
					break;
			}

			pointer += 2;
		}

		return string.Join(',', output);
	}
}
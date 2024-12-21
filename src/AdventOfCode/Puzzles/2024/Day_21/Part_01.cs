using System;
using System.Collections.Generic;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_21;

public class Part_01 : Day_21
{
    public override int Part => 1;

	protected override int MaxLayers => 4;

    private List<List<char>> _padInput;

	public override void PrepareData(List<string> input)
	{
		_padInput = input.To2DCharList();
	}

	public override void Solve()
	{
		var result = CalculateSumOfKeyPresses(_padInput);
		Console.WriteLine($"Sum of shortest lengths of 4 deep robot control times numpad value: {result}");
	}
}
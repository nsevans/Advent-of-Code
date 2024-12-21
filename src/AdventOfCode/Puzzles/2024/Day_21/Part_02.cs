using System;
using System.Collections.Generic;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_21;

public class Part_02 : Day_21
{
    public override int Part => 2;

    protected override int MaxLayers => 27;

    private List<List<char>> _padInput;
	
	public override void PrepareData(List<string> input)
	{
		_padInput = input.To2DCharList();
	}

	public override void Solve()
	{
		var result = CalculateSumOfKeyPresses(_padInput);
		Console.WriteLine($"Sum of shortest lengths of 27 deep robot control times numpad value: {result}");
	}	
}
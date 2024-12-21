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

	public override string ResultMessage => "Sum of shortest lengths of 27 deep robot control times numpad value";

	public override string GetResult()
	{
		return CalculateSumOfKeyPresses(_padInput).ToString();
	}	
}
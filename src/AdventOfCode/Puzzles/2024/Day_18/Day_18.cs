using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_18;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/18
/// 
/// Example/Test Input: 
///		5,4
///		4,2
///		4,5
///		3,0
///		2,1
///		6,3
///		2,4
///		1,5
///		0,6
///		3,3
///		2,6
///		5,1
///		1,2
///		5,5
///		2,5
///		6,5
///		1,4
///		0,4
///		6,4
///		1,1
///		6,1
///		1,0
///		0,5
///		1,6
///		2,0
/// </summary>
public abstract class Day_18 : BaseSolver
{
	public override string Title => "RAM Run";
	public override int Day => 18;
	public override int Year => 2024;

	protected static readonly int Width = 70;
	protected static readonly int Height = 70;
	protected static readonly (int x, int y) StartPosition = (0, 0);
	protected static readonly (int x, int y) EndPosition = (Width, Height);

	protected static List<(int x, int y)> ParseIncomingBytes(List<string> input)
	{
		return input.Select(l =>
		{
			var s = l.Split(',', StringSplitOptions.RemoveEmptyEntries);
			var x = int.Parse(s[0]);
			var y = int.Parse(s[1]);
			return (x, y);
		}).ToList();
	}

	protected static List<List<char>> GenerateMap()
	{
		var map = new List<List<char>>();
		for (var i = 0; i < Height + 1; i++)
		{
			map.Add(Enumerable.Repeat('.', Width + 1).ToList());
		}
		return map;
	}
}
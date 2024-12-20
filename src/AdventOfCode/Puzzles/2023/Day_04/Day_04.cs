using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2023.Day_04;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2023/day/4
/// 
/// Example/Test Input: 
///		Card   1: 82 41 56 54 18 62 29 55 34 20 | 37 14 10 80 58 11 65 96 90  8 59 32 53 21 98 83 17  9 87 25 71 77 70 73 24
/// </summary>
public abstract class Day_04 : BaseSolver
{
	public override string Title => "Scratchcards";
    public override int Day => 4;
    public override int Year => 2023;
}
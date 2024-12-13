using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2023.Puzzle_04;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this puzzle
/// 
/// Link to Puzzle: https://adventofcode.com/2023/day/4
/// 
/// Input Format: 
///		Card   1: 82 41 56 54 18 62 29 55 34 20 | 37 14 10 80 58 11 65 96 90  8 59 32 53 21 98 83 17  9 87 25 71 77 70 73 24
/// </summary>
public abstract class Puzzle_04 : BasePuzzleSolver
{
	public override string Title => "Scratchcards";
    public override int PuzzleNumber => 4;
    public override int PuzzleYear => 2023;
}
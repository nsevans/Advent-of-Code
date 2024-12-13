using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2023.Puzzle_01;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this puzzle
/// 
/// Link to Puzzle: https://adventofcode.com/2023/day/1
/// 
/// Input Format: 
///		mrjstg5onetwoeightgcczx8vgrgl
/// </summary>
public abstract class Puzzle_01 : BasePuzzleSolver
{
	public override string Title => "Trebuchet?!";
    public override int PuzzleNumber => 1;
    public override int PuzzleYear => 2023;

	protected static List<string> NumberStrings => ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
}
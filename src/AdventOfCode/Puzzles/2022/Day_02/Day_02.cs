using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2022.Day_02;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2022/day/2
///
/// Example/Test Input:
///     A Y
///     B X
///     C Z
/// </summary>
public abstract class Day_02 : BasePythonSolver
{
    public override string Title => "Rock Paper Scissors";
    public override int Day => 2;
    public override int Year => 2022;
}
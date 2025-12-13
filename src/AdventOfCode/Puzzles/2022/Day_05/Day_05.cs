using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2022.Day_05;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2022/day/5
///
/// Example/Test Input:
///         [D]
/// 	[N] [C]
///     [Z] [M] [P]
///      1   2   3
///
///     move 1 from 2 to 1
///     move 3 from 1 to 3
///     move 2 from 2 to 1
///     move 1 from 1 to 2
/// </summary>
public abstract class Day_05 : BasePythonSolver
{
    public override string Title => "Supply Stacks";
    public override int Day => 5;
    public override int Year => 2022;
}
using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2022.Day_01;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2022/day/1
///
/// Example/Test Input:
///     1000
///     2000
///     3000
///
///     4000
///
///     5000
///     6000
///
///     7000
///     8000
///     9000
///
///     10000
/// </summary>
public abstract class Day_01 : BasePythonSolver
{
    public override string Title => "Calorie Counting";
    public override int Day => 1;
    public override int Year => 2022;
}
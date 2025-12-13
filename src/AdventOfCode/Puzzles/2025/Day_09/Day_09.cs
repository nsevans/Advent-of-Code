using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2025.Day_09;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/9
///
/// Example/Test Input:
///     7,1
///     11,1
///     11,7
///     9,7
///     9,5
///     2,5
///     2,3
///     7,3
/// </summary>
public abstract class Day_09 : BasePythonSolver
{
    public override string Title => "Movie Theater";
    public override int Day => 9;
    public override int Year => 2025;
}
using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2025.Day_05;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/5
///
/// Example/Test Input:
///     3-5
///     10-14
///     16-20
///     12-18
///
///     1
///     5
///     8
///     11
///     17
///     32
/// </summary>
public abstract class Day_05 : BasePythonSolver
{
    public override string Title => "Cafeteria";
    public override int Day => 5;
    public override int Year => 2025;
}
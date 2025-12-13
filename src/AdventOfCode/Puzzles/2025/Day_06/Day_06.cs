using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2025.Day_06;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/6
///
/// Example/Test Input:
///     123 328  51 64
///      45 64  387 23
///       6 98  215 314
///     *   +   *   +
/// </summary>
public abstract class Day_06 : BasePythonSolver
{
    public override string Title => "Trash Compactor";
    public override int Day => 6;
    public override int Year => 2025;
}
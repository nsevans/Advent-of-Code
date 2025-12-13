using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2025.Day_04;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/4
///
/// Example/Test Input:
///     ..@@.@@@@.
///     @@@.@.@.@@
///     @@@@@.@.@@
///     @.@@@@..@.
///     @@.@@@@.@@
///     .@@@@@@@.@
///     .@.@.@.@@@
///     @.@@@.@@@@
///     .@@@@@@@@.
///     @.@.@@@.@.
/// </summary>
public abstract class Day_04 : BasePythonSolver
{
    public override string Title => "Printing Department";
    public override int Day => 4;
    public override int Year => 2025;
}
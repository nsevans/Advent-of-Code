using AdventOfCode.Puzzles.Solvers;

namespace AdventOfCode.Puzzles.Year_2025.Day_01;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/1
///
/// Example/Test Input:
///     L68
///     L30
///     R48
///     L5
///     R60
///     L55
///     L1
///     L99
///     R14
///     L82
/// </summary>
public abstract class Day_01 : BasePythonSolver
{
    public override string Title => "Secret Entrance";
    public override int Day => 1;
    public override int Year => 2025;
}
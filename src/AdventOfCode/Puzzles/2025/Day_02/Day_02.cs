using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2025.Day_02;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/2
///
/// Example/Test Input:
///     11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124
/// </summary>
public abstract class Day_02 : BasePythonSolver
{
    public override string Title => "Gift Shop";
    public override int Day => 2;
    public override int Year => 2025;
}
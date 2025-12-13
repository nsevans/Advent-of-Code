namespace AdventOfCode.Puzzles.Year_2022.Day_04;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2022/day/4
///
/// Example/Test Input:
///     2-4,6-8
///     2-3,4-5
///     5-7,7-9
///     2-8,3-7
///     6-6,4-6
///     2-6,4-8
/// </summary>
public abstract class Day_04 : BasePythonSolver
{
    public override string Title => "Camp Cleanup";
    public override int Day => 4;
    public override int Year => 2022;
}
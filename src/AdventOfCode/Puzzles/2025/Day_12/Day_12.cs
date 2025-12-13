namespace AdventOfCode.Puzzles.Year_2025.Day_12;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/12
///
/// Note: The example input does not work for this puzzle as the solution was tailored to work
///     with the real puzzle input (although it should work with ANY real puzzle input).
/// Example/Test Input:
///     0:
///     ###
///     ##.
///     ##.
///
///     1:
///     ###
///     ##.
///     .##
///
///     2:
///     .##
///     ###
///     ##.
///
///     3:
///     ##.
///     ###
///     ##.
///
///     4:
///     ###
///     #..
///     ###
///
///     5:
///     ###
///     .#.
///     ###
///
///     4x4: 0 0 0 0 2 0
///     12x5: 1 0 1 0 2 2
///     12x5: 1 0 1 0 3 2
/// </summary>
public abstract class Day_12 : BasePythonSolver
{
    public override string Title => "Christmas Tree Farm";
    public override int Day => 12;
    public override int Year => 2025;
}
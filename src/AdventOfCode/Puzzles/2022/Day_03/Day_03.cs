using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2022.Day_03;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2022/day/3
///
/// Example/Test Input:
///     vJrwpWtwJgWrhcsFMMfFFhFp
///     jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
///     PmmdzqPrVvPwwTWBwg
///     wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
///     ttgJtRGJQctTZtZT
///     CrZsJsPPZsGzwwsLwLmpwMDw
/// </summary>
public abstract class Day_03 : BasePythonSolver
{
    public override string Title => "Rucksack Reorganization";
    public override int Day => 3;
    public override int Year => 2022;
}
using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2025.Day_11;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2025/day/11
///
/// Example/Test Input:
/// Part 1 Input:
///     aaa: you hhh
///     you: bbb ccc
///     bbb: ddd eee
///     ccc: ddd eee fff
///     ddd: ggg
///     eee: out
///     fff: out
///     ggg: out
///     hhh: ccc fff iii
///     iii: out
///
/// Part 2 Input:
///     svr: aaa bbb
///     aaa: fft
///     fft: ccc
///     bbb: tty
///     tty: ccc
///     ccc: ddd eee
///     ddd: hub
///     hub: fff
///     eee: dac
///     dac: fff
///     fff: ggg hhh
///     ggg: out
///     hhh: out
/// </summary>
public abstract class Day_11 : BasePythonSolver
{
    public override string Title => "Reactor";
    public override int Day => 11;
    public override int Year => 2025;
}
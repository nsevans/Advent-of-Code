using System;
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Solvers;

public abstract class BaseCSharpSolver : BaseSolver
{
    public override TimeSpan TimePrepareDateExecution(List<string> input)
    {
        var loadStartTime = DateTime.Now;
        PrepareData(input);
        return DateTime.Now - loadStartTime;
    }

    public override TimeSpan TimeGetResultExecution(out string result)
    {
        var solveStartTime = DateTime.Now;
        result = GetResult();
        return DateTime.Now - solveStartTime;
    }
}
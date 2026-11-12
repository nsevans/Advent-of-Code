namespace AdventOfCode.Services.Solve.BaseSolvers;

public abstract class BaseCSharpSolver : BaseSolver
{
    public override TimeSpan PrepareDateExecutionTimed(List<string> input)
    {
        var loadStartTime = DateTime.Now;
        PrepareData(input);
        return DateTime.Now - loadStartTime;
    }

    public override TimeSpan GetResultExecutionTimed(out string result)
    {
        var solveStartTime = DateTime.Now;
        result = GetResult();
        return DateTime.Now - solveStartTime;
    }
}
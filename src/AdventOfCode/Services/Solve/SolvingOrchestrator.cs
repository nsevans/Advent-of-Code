namespace AdventOfCode.Services.Solve;

public class SolvingOrchestrator(SolverContext context)
{
    private readonly SolverContext _context = context;

    public void Run()
    {
        #region Collect Solvers

        if (_context.Verbose)
        {
            if (_context.HasFilters)
                Console.WriteLine($"Collecting puzzle solvers for {_context.YearText}, for {_context.DayText}, and for {_context.PartText}...");
            else
                Console.WriteLine("Collecting all puzzle solvers...");
        }

        var solverGroups = SolverFactory.GetGroupedSolvers(_context);

        if (_context.Verbose)
            Console.WriteLine($"Found {solverGroups.SelectMany(x => x).Count()} puzzle solvers after applying the given filters\n");

        #endregion

        #region Run Solvers

        var solvingService = new SolvingService(_context);
        var totalTime = solvingService.Run(solverGroups);

        var totalTimeOutput = $"Total Time for {solverGroups.SelectMany(x => x).Count()} puzzles: {totalTime.ToRoundedMilliseconds(4)} ms";
        Console.WriteLine(totalTimeOutput);
        if (_context.GenerateMarkdownTable)
        {
            using var tableOutputFile = new StreamWriter(_context.MarkdownTablePath);
            tableOutputFile.WriteLine(totalTimeOutput+"\n");
            tableOutputFile.Write(_context.MarkdownTableOutput);
        }

        #endregion
    }
}
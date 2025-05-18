using System;
using System.Collections.Generic;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Services.Solving;

public class SolvingService(SolverContext context)
{
    private readonly SolverContext _context = context;

    public void Run(List<List<BaseSolver>> solverGroups)
    {
        if (!_context.Verbose)
        {
            Console.WriteLine($"| {new string("Puzzle"),-15} | {new string("Total Time"),18} | {new string("Result"),-50} |");
            Console.WriteLine($"|{new string('-', 17)}+{new string('-',20)}+{new string('-',52)}|");
        }

        foreach (var group in solverGroups)
        {
            RunGroup(group);
            if (_context.Verbose)
                Console.WriteLine("\n#--------------------------------------------------#\n");
        }

        if (!_context.Verbose)
        {
            Console.WriteLine($"|{new string('-', 17)}-{new string('-',20)}-{new string('-',52)}|");
        }
    }

    private void RunGroup(List<BaseSolver> solverGroup)
    {
        foreach (var solver in solverGroup)
        {
            solver.DisplayHeading(_context.Verbose);

            if (!SolvingInputService.TryGetInput(solver, out var input))
            {
                Console.WriteLine($"Unable to read contents of file: '{solver.InputFilePath}'. The puzzle input can be found here: {solver.DownloadLink}");
                // Return from method instead of breaking as all solvers in the group would use the same input file
                return;
            }

            RunSolver(solver, input);
        }
    }

    private void RunSolver(BaseSolver solver, List<string> input)
    {
        var loadTime = PrepareSolverData(solver, input);
        var solveTime = GetSolverResult(solver, out var result);

        var totalTime = loadTime + solveTime;

        if (_context.Verbose)
        {
            Console.WriteLine($"-- {solver.ResultMessage.Trim()} --");
            Console.WriteLine($"-- Result: {result} --");
            Console.WriteLine($"Prep  Time: {loadTime.ToFormattedMilliseconds(15)} ms");
            Console.WriteLine($"Solve Time: {solveTime.ToFormattedMilliseconds(15)} ms");
            Console.WriteLine($"Total Time: {totalTime.ToFormattedMilliseconds(15)} ms");
        }
        else
        {
            Console.WriteLine($"{totalTime.ToFormattedMilliseconds(15)} ms | {result,-50} |");
        }
    }

    private TimeSpan PrepareSolverData(BaseSolver solver, List<string> input)
    {
        var loadStartTime = DateTime.Now;
        solver.PrepareData(input);
        var loadTime = DateTime.Now - loadStartTime;

        return loadTime;
    }

    private TimeSpan GetSolverResult(BaseSolver solver, out string result)
    {
        var solveStartTime = DateTime.Now;
        result = solver.GetResult();
        var solveTime = DateTime.Now - solveStartTime;

        return solveTime;
    }
}
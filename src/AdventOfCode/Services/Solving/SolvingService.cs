using System;
using System.Collections.Generic;
using System.Text.Json;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models.Contexts;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Services.Solving;

public class SolvingService(SolverContext context)
{
    private readonly SolverContext _context = context;

    public TimeSpan Run(List<List<BaseSolver>> solverGroups)
    {
        if (!_context.Verbose)
        {
            Console.WriteLine($"| {new string("Puzzle"),-15} | {new string("Language"),-10} | {new string("Total Time"),18} | {new string("Result"),-50} |");
            Console.WriteLine($"|{new string('-', 17)}+{new string('-',12)}+{new string('-',20)}+{new string('-',52)}|");
        }

        if (_context.GenerateMarkdownTable)
        {
            _context.MarkdownTableOutput.AppendLine($"| Year | Day | Part | Language | Title | Total Time |");
            _context.MarkdownTableOutput.AppendLine($"|:-----|:----|:-----|:---------|:------|-----------:|");
        }

        var totalRunTime = new TimeSpan();
        foreach (var group in solverGroups)
        {
            totalRunTime += RunGroup(group);
            if (_context.Verbose)
                Console.WriteLine("\n#--------------------------------------------------#\n");
        }

        if (!_context.Verbose)
        {
            Console.WriteLine($"|{new string('-', 17)}-{new string('-', 12)}-{new string('-', 20)}-{new string('-', 52)}|");
        }

        return totalRunTime;
    }

    private TimeSpan RunGroup(List<BaseSolver> solverGroup)
    {
        var totalGroupRunTime = new TimeSpan();
        foreach (var solver in solverGroup)
        {
            if (_context.Verbose)
                Console.WriteLine(solver.DetailedHeading);
            else
                Console.Write($"| {solver.MinimalHeading,-15} | {solver.GetSolvingLanguage(),-10} | ");

            if (!SolvingInputService.TryGetInput(solver, out var input))
            {
                Console.WriteLine($"Unable to read contents of file: '{solver.InputFilePath}'. The puzzle input can be found here: {solver.DownloadLink}");
                // Return from method instead of breaking as all solvers in the group would use the same input file
                return new TimeSpan();
            }

            totalGroupRunTime += RunSolver(solver, input);
        }

        return totalGroupRunTime;
    }

    private TimeSpan RunSolver(BaseSolver solver, List<string> input)
    {
        var loadTime = solver.TimePrepareDateExecution(input);
        var solveTime = solver.TimeGetResultExecution(out var result);

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

        if (_context.GenerateMarkdownTable)
            _context.MarkdownTableOutput.AppendLine($"| {solver.TableHeading} | {totalTime.ToRoundedMilliseconds(4)} ms |");

        return totalTime;
    }
}
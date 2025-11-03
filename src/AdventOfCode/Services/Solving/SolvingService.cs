using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Models;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Services.Solving;

public class SolvingService(SolverContext context)
{
    private readonly SolverContext _context = context;

    private static readonly Dictionary<Type, TimeSpan> _processOverheadTimes = [];
    private static TimeSpan _usedOverheadTime = new();

    public TimeSpan Run(List<List<BaseSolver>> solverGroups)
    {
        CalculateProcessOverheadTimes();

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
        var loadTime = PrepareSolverData(solver, input);
        var solveTime = GetSolverResult(solver, out var result);

        if (_processOverheadTimes.TryGetValue(solver.GetType().BaseType.BaseType, out var overheadTime))
        {
            solveTime -= overheadTime;
            _usedOverheadTime += overheadTime;
        }

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

    private static TimeSpan PrepareSolverData(BaseSolver solver, List<string> input)
    {
        var loadStartTime = DateTime.Now;
        solver.PrepareData(input);
        var loadTime = DateTime.Now - loadStartTime;

        return loadTime;
    }

    private static TimeSpan GetSolverResult(BaseSolver solver, out string result)
    {
        var solveStartTime = DateTime.Now;
        result = solver.GetResult();
        var solveTime = DateTime.Now - solveStartTime;

        return solveTime;
    }

    /**
    * This is temporary
    * TODO: Figure out a new way of either running python scripts via C#, or a better way of calculating python puzzle execution time
    * For the mean time, getting an average process start and close time is sufficient.
    */
    private static void CalculateProcessOverheadTimes()
    {
        _processOverheadTimes.Add(typeof(BasePythonSolver), GetPythonProcessOverheadTime());
    }

    private static TimeSpan GetPythonProcessOverheadTime()
    {
        var pythonProcessOverheadTime = new TimeSpan();
        var count = 10;
        for (var i = 0; i < count; i++)
        {
            var process = new Process()
            {
                StartInfo = new()
                {
                    FileName = "python",
                    UseShellExecute = false,
                    Arguments = "-c \"1 + 1\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = false
                }
            };

            var startTime = DateTime.Now;
            process.Start();
            process.BeginOutputReadLine();
            pythonProcessOverheadTime += DateTime.Now - startTime;

            process.WaitForExit();
            process.Close();
        }

        pythonProcessOverheadTime /= count;
        return pythonProcessOverheadTime;
    }
}
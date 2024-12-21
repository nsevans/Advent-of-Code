using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Services;

public class SolvingService(SolverContext context)
{
	private readonly SolverContext _context = context;

    public void Run(List<List<BaseSolver>> solverGroups)
	{
		foreach (var group in solverGroups)
		{
			RunGroup(group);
			if (_context.Verbose)
				Console.WriteLine("#--------------------------------------------------#");
		}
	}

	private void RunGroup(List<BaseSolver> solverGroup)
	{
		foreach (var solver in solverGroup)
		{
			solver.DisplayHeading(_context.Verbose);
			RunSolver(solver);
		}
	}

	private void RunSolver(BaseSolver solver)
	{
		var input = InputService.GetInput(solver);

		var loadTime = PrepareSolverData(solver, input);
		var solveTime = GetSolverResult(solver, out var result);

		var totalTime = loadTime + solveTime;

		if (_context.Verbose)
		{
			Console.WriteLine($"-- {solver.ResultMessage} --");
			Console.WriteLine($"-- Result:  {result,20} --");
			Console.WriteLine($"Prep  Time: {loadTime.ToFormattedMilliseconds(20)} ms");
			Console.WriteLine($"Solve Time: {solveTime.ToFormattedMilliseconds(20)} ms");
			Console.WriteLine($"Total Time: {totalTime.ToFormattedMilliseconds(20)} ms\n");
		}
		else
		{
			Console.WriteLine($" | {result,25} | {totalTime.ToFormattedMilliseconds(20)} ms");
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
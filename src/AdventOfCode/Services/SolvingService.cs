using System;
using System.Collections.Generic;
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

		var loadStartTime = DateTime.Now;
		solver.PrepareData(input);
		var loadTime = DateTime.Now - loadStartTime;

		var solveStartTime = DateTime.Now;
		solver.Solve();
		var solveTime = DateTime.Now - solveStartTime;

		var totalTime = loadTime + solveTime;

		if (_context.Verbose)
		{
			Console.WriteLine($"## {solver.ResultMessage}: {solver.Result}");
			Console.WriteLine($"Prep  Time: 	{loadTime.TotalMilliseconds} ms");
			Console.WriteLine($"Solve Time:     {solveTime.TotalMilliseconds} ms");
			Console.WriteLine($"Total Time:     {totalTime.TotalMilliseconds} ms\n");
		}
		else
		{
			Console.WriteLine($" - {totalTime.TotalMilliseconds} ms");
		}
	}
}
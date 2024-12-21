using System;
using System.Linq;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Factories;

namespace AdventOfCode.Services;

public class SolvingOrchestrator(SolverContext context)
{
	private readonly SolverContext _context = context;

	public void Run()
	{
		#region Collect Solvers

		if (_context.Verbose)
		{
			if (_context.NoFiltersSpecified())
				Console.WriteLine("Collecting all puzzle solvers...");
			else
				Console.WriteLine($"Collecting puzzle solvers for {_context.YearText}, for {_context.DayText}, and for {_context.PartText}...");
		}

		var solverGroups = SolverFactory.GetGroupedSolvers(_context);
		
		if (_context.Verbose)
			Console.WriteLine($"Found {solverGroups.SelectMany(x => x).Count()} puzzle solvers after applying the given filters\n");

		#endregion

		#region Run Solvers

		var startTotalTime = DateTime.Now;

		var solvingService = new SolvingService(_context);
		solvingService.Run(solverGroups);

		var totalTime = DateTime.Now - startTotalTime;
		Console.WriteLine($"Total Time: {totalTime.ToRoundedMilliseconds(4)} ms");

		#endregion
	}
}
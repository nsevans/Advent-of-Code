using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AdventOfCode.Puzzles;

namespace AdventOfCode;

public class SolvingService
{
	private readonly int? _year;
	private readonly int? _day;
	private readonly int? _part;
	private readonly bool _verbose;

	public SolvingService(int? year, int? day, int? part, bool verbose)
	{
		_year = year;
		_day = day;
		_part = part;
		_verbose = verbose;
	}

	public void Solve()
	{
		var solverGroups = GetSolvers();
		Console.WriteLine($"Found {solverGroups.SelectMany(x => x).Count()} puzzles");

		foreach (var group in solverGroups)
		{
			SolveGroup(group);
			Console.WriteLine("#--------------------------------------------------#");
		}
	}

	private void SolveGroup(List<BaseSolver> solverGroup)
	{
		var firstSolver = solverGroup.First();
		if (solverGroup.Count > 1)
			Console.WriteLine($"\n#### Solving {firstSolver.Year}, Day {firstSolver.Day}: '{firstSolver.Title}' ####");
		else
			Console.WriteLine($"\n### Solving {firstSolver.Year}, Day {firstSolver.Day}, Part {firstSolver.Part}: '{firstSolver.Title}' ####");

		Console.WriteLine($"Link: {solverGroup.First().DayLink}\n");

		foreach (var solver in solverGroup)
		{
			if (solverGroup.Count > 1)
				Console.WriteLine($"## Part {solver.Part} ##");

			Solve(solver);
			Console.WriteLine();
		}
	}

	private void Solve(BaseSolver solver)
	{
		var input = GetInput(solver);

		var loadStartTime = DateTime.Now;
		solver.PrepareData(input);
		var loadTime = DateTime.Now - loadStartTime;

		var solveStartTime = DateTime.Now;
		solver.Solve();
		var solveTime = DateTime.Now - solveStartTime;

		var totalTime = loadTime + solveTime;

		Console.WriteLine($"Prep  Time: 	{loadTime.TotalMilliseconds} ms");
		Console.WriteLine($"Solve Time:     {solveTime.TotalMilliseconds} ms");
		Console.WriteLine($"Total Time:     {totalTime.TotalMilliseconds} ms");
	}

	private List<List<BaseSolver>> GetSolvers()
	{
		var solvers = Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(t => t.IsClass)
			.Where(t => !t.IsAbstract)
			.Where(t => t.IsAssignableTo(typeof(BaseSolver)))
			.Where(t => t.Namespace.StartsWith("AdventOfCode.Puzzles."));

		// Filter out years
		if (_year != null)
			solvers = solvers
				.Where(t => t.Namespace.Contains($"Year_{_year}"));

		// Filter out day numbers
		if (_day != null)
		{
			var paddedDay = _day.ToString().Length == 1 ? $"0{_day}" : _day.ToString();
			solvers = solvers
				.Where(t => t.Namespace.EndsWith($"Day_{paddedDay}"));
		}

		// Filter out parts (1 or 2)
		if (_part != null)
			solvers = solvers
				.Where(t => t.Name == $"Part_0{_part}");

		// Group solvers by year and day, and ensure they're ordered properly
		var solverGroups = solvers
			.GroupBy(t => t.Namespace)
			.OrderBy(t => t.Key)
			.Select(g => g
				.Select(x => (BaseSolver) Activator.CreateInstance(x))
				.OrderBy(t => t.GetType().Name)
				.ToList())
			.ToList();

		return solverGroups;
	}

	private List<string> GetInput(BaseSolver solver)
	{
		var paddedDay = solver.Day.ToString().Length == 1 ? $"0{solver.Day}" : solver.Day.ToString();
		var fileName = $"./inputs/input_{solver.Year}_{paddedDay}.in";

		if (!File.Exists(fileName))
		{
			File.Create(fileName);	// Create file for easier setup if one isn't created
			throw new InvalidOperationException($"Ensure input exists for this day under '{Path.GetFullPath(fileName)}'.\nInput can be downloaded from {solver.DownloadLink}.");
		}

		var input = File.ReadLines(fileName).ToList();

		if (input.Count == 0 || string.IsNullOrWhiteSpace(string.Join("", input)))
			throw new InvalidOperationException($"Ensure the input file '{Path.GetFullPath(fileName)}' is not empty.\nInput can be downloaded from {solver.DownloadLink}.");

		return input;
	}
}
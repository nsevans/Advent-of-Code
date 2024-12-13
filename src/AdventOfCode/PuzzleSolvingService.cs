using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode;

public class PuzzleSolvingService
{
	private readonly int? _year;
	private readonly int? _day;
	private readonly int? _part;
	private readonly bool _verbose;

	public PuzzleSolvingService(int? year, int? day, int? part, bool verbose)
	{
		_year = year;
		_day = day;
		_part = part;
		_verbose = verbose;
	}

	public void SolvePuzzles()
	{
		var puzzleSolverGroups = GetPuzzleSolvers();
		Console.WriteLine($"Found {puzzleSolverGroups.SelectMany(x => x).Count()} puzzles");

		foreach (var group in puzzleSolverGroups)
		{
			SolvePuzzleGroup(group);
			Console.WriteLine("#--------------------------------------------------#");
		}
	}

	private void SolvePuzzleGroup(List<BasePuzzleSolver> puzzleSolverGroup)
	{
		var firstSolver = puzzleSolverGroup.First();
		if (puzzleSolverGroup.Count > 1)
			Console.WriteLine($"\n#### Solving {firstSolver.PuzzleYear}, Day {firstSolver.PuzzleNumber}: '{firstSolver.Title}' ####");
		else
		{
			var part = firstSolver.GetType().Name[^1];
			Console.WriteLine($"\n### Solving {firstSolver.PuzzleYear}, Day {firstSolver.PuzzleNumber}, Part {part}: '{firstSolver.Title}' ####");
		}

		Console.WriteLine($"Link: {puzzleSolverGroup.First().PuzzleLink}\n");

		foreach (var puzzleSolver in puzzleSolverGroup)
		{
			if (puzzleSolverGroup.Count > 1)
				Console.WriteLine($"## Part {puzzleSolver.GetType().Name[^1]} ##");

			SolvePuzzle(puzzleSolver);

			Console.WriteLine();
		}
	}

	private void SolvePuzzle(BasePuzzleSolver puzzleSolver)
	{
		var puzzleInput = GetInput(puzzleSolver);

		var loadStartTime = DateTime.Now;
		puzzleSolver.PrepareData(puzzleInput);
		var loadTime = DateTime.Now - loadStartTime;

		var solveStartTime = DateTime.Now;
		puzzleSolver.Solve();
		var solveTime = DateTime.Now - solveStartTime;

		var totalTime = loadTime + solveTime;

		Console.WriteLine($"Data Prep Time: {loadTime.TotalMilliseconds} ms");
		Console.WriteLine($"Solve Time:     {solveTime.TotalMilliseconds} ms");
		Console.WriteLine($"Total Time:     {totalTime.TotalMilliseconds} ms");
	}

	private List<List<BasePuzzleSolver>> GetPuzzleSolvers()
	{
		var solvers = Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(t => t.IsClass)
			.Where(t => !t.IsAbstract)
			.Where(t => t.IsAssignableTo(typeof(BasePuzzleSolver)))
			.Where(t => t.Namespace.StartsWith("AdventOfCode.Puzzles."));

		// Filter out years
		if (_year != null)
			solvers = solvers
				.Where(t => t.Namespace.Contains($"Year_{_year}"));

		// Filter out puzzle numbers
		if (_day != null)
		{
			var paddedDay = _day.ToString().Length == 1 ? $"0{_day}" : _day.ToString();
			solvers = solvers
				.Where(t => t.Namespace.EndsWith($"Puzzle_{paddedDay}"));
		}

		// Filter out parts (1 or 2)
		if (_part != null)
			solvers = solvers
				.Where(t => t.Name == $"Part_0{_part}");

		// Group solvers by year and puzzle, and ensure they're ordered properly
		var solverGroups = solvers
			.GroupBy(t => t.Namespace)
			.OrderBy(t => t.Key)
			.Select(g => g
				.Select(x => (BasePuzzleSolver) Activator.CreateInstance(x))
				.OrderBy(t => t.GetType().Name)
				.ToList())
			.ToList();

		return solverGroups;
	}

	private List<string> GetInput(BasePuzzleSolver puzzleSolver)
	{
		var paddedPuzzle = puzzleSolver.PuzzleNumber.ToString().Length == 1 ? $"0{puzzleSolver.PuzzleNumber}" : puzzleSolver.PuzzleNumber.ToString();
		var fileName = $"./inputs/input_{puzzleSolver.PuzzleYear}_{paddedPuzzle}.in";

		if (!File.Exists(fileName))
		{
			File.Create(fileName);	// Create file for easier setup if one isn't created
			throw new InvalidOperationException($"Ensure input exists for this puzzle under {Path.GetFullPath(fileName)}.\nInput can be downloaded from {puzzleSolver.DownloadLink}.");
		}

		var input = File.ReadLines(fileName).ToList();

		if (input.Count == 0 || string.IsNullOrWhiteSpace(string.Join("", input)))
			throw new InvalidOperationException($"Ensure the input file {Path.GetFullPath(fileName)} is not empty.\nInput can be downloaded from {puzzleSolver.DownloadLink}.");

		return input;
	}
}
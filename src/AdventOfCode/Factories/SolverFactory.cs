using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode.Common.Models.Contexts;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Factories;

public static class SolverFactory
{
	/// <summary>
	/// Get a list of grouped solvers, grouped by year and day, ordered by year, day, and part, and
	/// filtered by the given context.
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	public static List<List<BaseSolver>> GetGroupedSolvers(SolverContext context)
	{
		var solverTypes = GetSolverTypes(context);

		// Group solvers by year and day, and ensure they're ordered properly (Year -> Day -> Part) inside list and inside group
		var solverGroups = solverTypes
			.GroupBy(t => t.Namespace)
			.OrderBy(t => t.Key)
			.Select(g => g
				.Select(x => (BaseSolver) Activator.CreateInstance(x))
				.OrderBy(t => t.GetType().Name)
				.ToList())
			.ToList();

		return solverGroups;
	}

	/// <summary>
	/// Get a list of solvers ordered by year, day, and part and filtered by the given context.
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	public static List<BaseSolver> GetSolvers(SolverContext context)
	{
		var solverTypes = GetSolverTypes(context);

		return solverTypes
			.Select(x => (BaseSolver) Activator.CreateInstance(x))
			.OrderBy(t => t.GetType().FullName)
			.ToList();
	}

    /// <summary>
    /// Get a list filtered list of solvers from the given context
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private static IEnumerable<Type> GetSolverTypes(SolverContext context)
    {
        var solvers = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass)
            .Where(t => !t.IsAbstract)
            .Where(t => t.IsAssignableTo(typeof(ISolver)))
            .Where(t => t.Namespace.StartsWith("AdventOfCode.Puzzles."));

        // Filter out years
        if (context.Year != null)
            solvers = solvers
                .Where(t => t.Namespace.Contains($"Year_{context.Year}"));

        // Filter out day numbers
        if (context.Day != null)
        {
            // 1 => 01, 10 => 10
            var paddedDay = context.Day.ToString().PadLeft(2, '0');
            solvers = solvers
                .Where(t => t.Namespace.EndsWith($"Day_{paddedDay}"));
        }

        // Filter out parts (1 or 2)
        if (context.Part != null)
            solvers = solvers
                .Where(t => t.Name == $"Part_0{context.Part}");

        return solvers;
    }
}
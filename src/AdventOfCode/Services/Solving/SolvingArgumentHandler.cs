using System;
using AdventOfCode.Common.Constants;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Services;
using AdventOfCode.Models;

namespace AdventOfCode.Services.Solving;

public class SolvingArgumentHandler : IInputHandler<SolverContext>
{
	public static SolverContext HandleInput(string[] args)
	{
		try
		{
			var yearValue = args.GetValueForArgument(SolveCommandConstants.Year.Options);
			var year = string.IsNullOrEmpty(yearValue) ? (int?)null : int.Parse(yearValue);

			var dayValue = args.GetValueForArgument(SolveCommandConstants.Day.Options);
			var day = string.IsNullOrEmpty(dayValue) ? (int?)null : int.Parse(dayValue);

			var partValue = args.GetValueForArgument(SolveCommandConstants.Part.Options);
			var part = string.IsNullOrEmpty(partValue) ? (int?)null : int.Parse(partValue);

			var verbose = args.HasArgument(SolveCommandConstants.Verbose.Options);

			var markdownOutputPath = args.GetValueForArgument(SolveCommandConstants.MarkdownOutput.Options);

			var context = new SolverContext(year, day, part, verbose, markdownOutputPath);
			return context;
		}
		catch (ArgumentException ae)
		{
			Console.WriteLine(ae.Message);
			Environment.Exit(0);
		}

		return null;
	}
}
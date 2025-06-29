using System;
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
			var yearValue = args.GetValueForArgument(["-y", "--year"]);
			var year = string.IsNullOrEmpty(yearValue) ? (int?)null : int.Parse(yearValue);

			var dayValue = args.GetValueForArgument(["-d", "--day"]);
			var day = string.IsNullOrEmpty(dayValue) ? (int?)null : int.Parse(dayValue);

			var partValue = args.GetValueForArgument(["-p", "--part"]);
			var part = string.IsNullOrEmpty(partValue) ? (int?)null : int.Parse(partValue);

			var verbose = args.HasArgument(["-v", "--verbose"]);

			var markdownOutputPath = args.GetValueForArgument(["-o", "--markdown-output"]);

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
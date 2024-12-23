using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Common.Services;

namespace AdventOfCode.Services.Solving;

public class SolvingArgumentHandler : IInputHandler<SolverContext>
{
	public static SolverContext HandleInput(string[] args)
	{
		var yearValue = args.GetValueForArgument("--year");
		var year = string.IsNullOrEmpty(yearValue) ? (int?) null : int.Parse(yearValue);

		var dayValue = args.GetValueForArgument("--day");
		var day = string.IsNullOrEmpty(dayValue) ? (int?) null : int.Parse(dayValue);

		var partValue = args.GetValueForArgument("--part");
		var part = string.IsNullOrEmpty(partValue) ? (int?)null : int.Parse(partValue);

		var verbose = args.HasArgument("--verbose");

		var context = new SolverContext(year, day, part, verbose);
		return context;
	}
}
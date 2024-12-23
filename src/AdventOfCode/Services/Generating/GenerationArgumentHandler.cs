using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Common.Services;

namespace AdventOfCode.Services.Generating;

public class GenerationArgumentHandler : IInputHandler<GeneratorContext>
{
	public static GeneratorContext HandleInput(string[] args)
	{
		var yearValue = args.GetValueForArgument("--year", true);
		var year = int.Parse(yearValue);

		var dayValue = args.GetValueForArgument("--day", true);
		var day = int.Parse(dayValue);

		var context = new GeneratorContext(year, day);

		return context;
	}
}
using System;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Services;
using AdventOfCode.Models;

namespace AdventOfCode.Services.Generating;

public class GenerationArgumentHandler : IInputHandler<GeneratorContext>
{
	public static GeneratorContext HandleInput(string[] args)
	{
		try
		{
			var yearValue = args.GetValueForArgument(["-y", "--year"], true);
			var year = int.Parse(yearValue);

			var dayValue = args.GetValueForArgument(["-d", "--day"], true);
			var day = int.Parse(dayValue);

			var title = args.GetValueForArgument(["-t", "--title"]) ?? "";

			var context = new GeneratorContext(year, day, title);
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
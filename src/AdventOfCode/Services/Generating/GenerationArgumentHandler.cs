using System;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Common.Services;

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

			var context = new GeneratorContext(year, day);
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
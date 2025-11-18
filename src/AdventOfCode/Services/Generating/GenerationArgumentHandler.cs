using System;
using AdventOfCode.Common.Constants;
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
            var yearValue = args.GetValueForArgument(GenerateCommandConstants.Year.Options, isRequired: true);
            var year = int.Parse(yearValue);

            var dayValue = args.GetValueForArgument(GenerateCommandConstants.Day.Options, isRequired: true);
            var day = int.Parse(dayValue);

            var title = args.GetValueForArgument(GenerateCommandConstants.Title.Options) ?? "";

            var language = args
                .GetValueForArgument(GenerateCommandConstants.Language.Options,
                    isRequired: true,
                    allowedValues: GenerateCommandConstants.Language.AcceptedValues)
                .ToLower();

            var context = new GeneratorContext(year, day, title, language);
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
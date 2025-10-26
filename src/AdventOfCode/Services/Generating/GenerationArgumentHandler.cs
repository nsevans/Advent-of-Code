using System;
using System.Linq;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Services;
using AdventOfCode.Models;

namespace AdventOfCode.Services.Generating;

public class GenerationArgumentHandler : IInputHandler<GeneratorContext>
{
    private static readonly string[] ValidLanguages = ["c#", "dotnet", "python"];

	public static GeneratorContext HandleInput(string[] args)
    {
        try
        {
            var yearValue = args.GetValueForArgument(["-y", "--year"], isRequired: true);
            var year = int.Parse(yearValue);

            var dayValue = args.GetValueForArgument(["-d", "--day"], isRequired: true);
            var day = int.Parse(dayValue);

            var title = args.GetValueForArgument(["-t", "--title"]) ?? "";

            var language = args.GetValueForArgument(["-l", "--language"], isRequired: true, allowedValues: ValidLanguages).ToLower();
            language = language == "c#" ? "dotnet" : language;

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
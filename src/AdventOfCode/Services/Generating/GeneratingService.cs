using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Common.Models.Enums;
using AdventOfCode.Models;

namespace AdventOfCode.Services.Generating;

public class GeneratingService(GeneratorContext context)
{
	private readonly GeneratorContext _context = context;

	public void Generate()
	{
		var outputDirectory = $"./Puzzles/{_context.Year}/Day_{_context.DisplayDay}";

		Console.WriteLine($"Generating {_context.Language} files for day {_context.DisplayDay} of {_context.Year}");

		if (Directory.Exists(outputDirectory) && Directory.EnumerateFileSystemEntries(outputDirectory).Any())
		{
			Console.WriteLine($"Folder '{outputDirectory}' is not empty, cannot generate classes.");
			return;
		}

		if (!Directory.Exists(outputDirectory))
			Directory.CreateDirectory(outputDirectory);

        if (_context.Language.ToLower() == "dotnet")
            GenerateDotnetTemplates(outputDirectory, _context);
        else if (_context.Language.ToLower() == "python")
            GeneratePythonTemplates(outputDirectory, _context);
        else
            throw new InvalidOperationException($"Invalid language {_context.Language}.");

		Console.WriteLine($"Finished generating files for day {_context.DisplayDay} of {_context.Year}");
	}

    private static void GenerateDotnetTemplates(string outputDirectory, GeneratorContext context)
    {
        var dayTemplate = LoadDotnetTemplate(TemplateType.Day, context.Language);
		var partTemplate = LoadDotnetTemplate(TemplateType.Part, context.Language);

		var dayClass = dayTemplate
			.Replace("<year>", context.Year.ToString())
			.Replace("<day>", context.Day.ToString())
			.Replace("<displayday>", context.DisplayDay)
			.Replace("<title>", context.Title);

		var partClasses = new List<string>();
		for (var i = 1; i <= 2; i++)
		{
			var partClass = partTemplate
				.Replace("<year>", context.Year.ToString())
				.Replace("<day>", context.Day.ToString())
			    .Replace("<displayday>", context.DisplayDay)
				.Replace("<part>", i.ToString())
				.Replace("<displaypart>", i.ToString().PadLeft(2, '0'));

			partClasses.Add(partClass);
		}

		File.WriteAllText($"{outputDirectory}/Day_{context.DisplayDay}.cs", dayClass);
		File.WriteAllText($"{outputDirectory}/Part_01.cs", partClasses[0]);
		File.WriteAllText($"{outputDirectory}/Part_02.cs", partClasses[1]);
    }

    private static void GeneratePythonTemplates(string outputDirectory, GeneratorContext context)
    {
        var dayTemplate = LoadDotnetTemplate(TemplateType.Day, context.Language);
        var partDotnetTemplate = LoadDotnetTemplate(TemplateType.Part, context.Language);
        var partPythonTemplate = LoadPythonTemplate(TemplateType.Part, context.Language);

        var dayClass = dayTemplate
            .Replace("<year>", context.Year.ToString())
            .Replace("<day>", context.Day.ToString())
			.Replace("<displayday>", context.DisplayDay)
            .Replace("<title>", context.Title);

        var partClasses = new List<string>();
        for (var i = 1; i <= 2; i++)
        {
            var partClass = partDotnetTemplate
                .Replace("<year>", context.Year.ToString())
                .Replace("<day>", context.Day.ToString())
			    .Replace("<displayday>", context.DisplayDay)
                .Replace("<part>", i.ToString())
                .Replace("<displaypart>", i.ToString().PadLeft(2, '0'));

            partClasses.Add(partClass);
        }

        var partScripts = new List<string>();
        for (var i = 1; i <= 2; i++)
        {
            var partScript = partPythonTemplate
                .Replace("<year>", context.Year.ToString())
                .Replace("<day>", context.Day.ToString())
			    .Replace("<displayday>", context.DisplayDay)
                .Replace("<part>", i.ToString())
                .Replace("<displaypart>", i.ToString().PadLeft(2, '0'));

            partScripts.Add(partScript);
        }

        File.WriteAllText($"{outputDirectory}/Day_{context.DisplayDay}.cs", dayClass);
		File.WriteAllText($"{outputDirectory}/Part_01.cs", partClasses[0]);
		File.WriteAllText($"{outputDirectory}/Part_01.py", partScripts[0]);
		File.WriteAllText($"{outputDirectory}/Part_02.cs", partClasses[1]);
		File.WriteAllText($"{outputDirectory}/Part_02.py", partScripts[1]);
    }

    private static string LoadPythonTemplate(TemplateType templateType, string language)
    {
        return File.ReadAllText($"./Services/Generating/Templates/{language}/{Enum.GetName(typeof(TemplateType), templateType)}.py.template");
    }

    private static string LoadDotnetTemplate(TemplateType templateType, string language)
    {
        return File.ReadAllText($"./Services/Generating/Templates/{language}/{Enum.GetName(typeof(TemplateType), templateType)}.cs.template");
    }
}
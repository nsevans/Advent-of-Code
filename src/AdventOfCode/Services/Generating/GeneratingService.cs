using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Common.Models;
using AdventOfCode.Common.Models.Enums;

namespace AdventOfCode.Services.Generating;

public class GeneratingService(GeneratorContext context)
{
	private readonly GeneratorContext _context = context;

	public void Generate()
	{
		var outputDirectory = $"./Puzzles/{_context.Year}/Day_{_context.DisplayDay}";

		Console.WriteLine($"Generating files for day {_context.Day} of {_context.Year}");

		if (Directory.Exists(outputDirectory) && Directory.EnumerateFileSystemEntries(outputDirectory).Any())
		{
			Console.WriteLine($"Folder '{outputDirectory}' is not empty, cannot generate classes.");
			return;
		}

		if (!Directory.Exists(outputDirectory))
			Directory.CreateDirectory(outputDirectory);

		var dayTemplate = LoadTemplate(TemplateType.Day);
		var partTemplate = LoadTemplate(TemplateType.Part);

		var dayClass = dayTemplate
			.Replace("<year>", _context.Year.ToString())
			.Replace("<day>", _context.Day.ToString());

		var partClasses = new List<string>();
		for (var i = 1; i <= 2; i++)
		{
			var partClass = partTemplate
				.Replace("<year>", _context.Year.ToString())
				.Replace("<day>", _context.Day.ToString())
				.Replace("<part>", i.ToString());

			partClasses.Add(partClass);
		}

		File.WriteAllText($"{outputDirectory}/Day_{_context.DisplayDay}.cs", dayClass);
		File.WriteAllText($"{outputDirectory}/Part_01.cs", partClasses[0]);
		File.WriteAllText($"{outputDirectory}/Part_02.cs", partClasses[1]);

		Console.WriteLine($"Finished generating files for day {_context.Day} of {_context.Year}");
	}

	private static string LoadTemplate(TemplateType templateType)
	{
		return File.ReadAllText($"./Services/Generating/Templates/{Enum.GetName(typeof(TemplateType), templateType)}.cs.template");
	}
}
using System;
using System.IO;
using System.Linq;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models.Contexts;

namespace AdventOfCode.Services.Generate.Generators;

public abstract class BasePuzzleGenerator(GeneratorContext context) : IPuzzleGenerator
{
    protected readonly GeneratorContext _context = context;

    protected abstract string TemplateFolderName { get; }

    private string TemplateFilePath => $"./Services/Generate/Templates/{TemplateFolderName}";

    protected string OutputDirectory => $"./Puzzles/{_context.Year}/Day_{_context.DisplayDay}";

    public bool GenerateTemplates()
    {
        Console.WriteLine($"Generating {_context.Language} files for day {_context.DisplayDay} of {_context.Year}");

        if (!Directory.Exists(OutputDirectory))
        {
            Directory.CreateDirectory(OutputDirectory);
        }
        else if (Directory.EnumerateFileSystemEntries(OutputDirectory).Any())
        {
            Console.WriteLine($"Folder '{OutputDirectory}' is not empty, cannot generate classes.");
            return false;
        }

        return GenerateTemplatesInternal();
    }

    protected abstract bool GenerateTemplatesInternal();

    protected bool TryLoadTemplate(string fileName, out string fileContents)
    {
        fileContents = string.Empty;
        var filePath = Path.Combine(TemplateFilePath, fileName);
        try
        {
            fileContents = File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read contents of {filePath}.\n{ex.Message}");
            return false;
        }

        return true;
    }

    protected string PopulateFile(string dayFileContents, int? partNumber = null)
    {
        var populatedFile = dayFileContents
            .Replace("<year>", _context.Year.ToString())
            .Replace("<day>", _context.Day.ToString())
            .Replace("<displayday>", _context.DisplayDay)
            .Replace("<title>", _context.Day.ToString());

        if (partNumber != null)
            populatedFile = populatedFile
                .Replace("<part>", partNumber.Value.ToString())
                .Replace("<displaypart>", partNumber.Value.PadLeft(2, '0'));

        return populatedFile;
    }
}
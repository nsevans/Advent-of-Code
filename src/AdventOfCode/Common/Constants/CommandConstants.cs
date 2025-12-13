using System;
using AdventOfCode.Common.Models;
using AdventOfCode.Common.Models.Enums;

namespace AdventOfCode.Common.Constants;

public static class SolveCommandConstants
{
    public static readonly Command Solve = new()
    {
        Name = "solve",
        Shortcut = "s",
        Description = "Solve puzzles and display their results."
    };

    public static readonly Command Verbose = new()
    {
        Name = "verbose",
        Shortcut = "v",
        Description = "[Optional] Print more verbose logs."
    };

    public static readonly Command MarkdownOutput = new()
    {
        Name = "markdown-output",
        Shortcut = "o",
        AcceptedType = typeof(string),
        Description = "[Optional] Generate a markdown table of puzzle runtimes and output the table to the given file path."
    };

    public static readonly Command Year = new()
    {
        Name = "year",
        Shortcut = "y",
        AcceptedType = typeof(int),
        Description = "[Optional] Specify the year of puzzles to run. Can be combined with the part and day arguments for a more refined filter."
    };

    public static readonly Command Day = new()
    {
        Name = "day",
        Shortcut = "d",
        AcceptedType = typeof(int),
        Description = "[Optional] Specify the day of puzzles to run. Can be combined with the part and year arguments for a more refined filter."
    };

    public static readonly Command Part = new()
    {
        Name = "part",
        Shortcut = "p",
        AcceptedType = typeof(int),
        Description = "[Optional] Specify the part of puzzles to run. Can be combined with the day and year arguments for a more refined filter."
    };

    public static readonly string MainCommandHelp = $"{Solve}";
    public static readonly string SubCommandHelp = $"{Year}\n{Day}\n{Part}\n{Verbose}\n{MarkdownOutput}";
}

public static class GenerateCommandConstants
{
    public static readonly Command Generate = new()
    {
        Name = "generate",
        Shortcut = "g",
        Description = "Generate a new puzzle tempalte from the available templates."
    };

    public static readonly Command Title = new()
    {
        Name = "title",
        Shortcut = "t",
        AcceptedType = typeof(string),
        Description = "[Required] Specify the title of the puzzle."
    };

    public static readonly Command Language = new()
    {
        Name = "language",
        Shortcut = "l",
        AcceptedType = typeof(string),
        AcceptedValues = Enum.GetNames<Language>(),
        Description = "[Required] Specify the programming language the puzzle template is generated for."
    };

    public static readonly Command Year = new()
    {
        Name = "year",
        Shortcut = "y",
        AcceptedType = typeof(int),
        Description = "[Required] Specify the year the puzzle template is generated for."
    };

    public static readonly Command Day = new()
    {
        Name = "day",
        Shortcut = "d",
        AcceptedType = typeof(int),
        Description = "[Required] Specify the day the puzzle template is generated for."
    };

    public static readonly string MainCommandHelp = $"{Generate}";
    public static readonly string SubCommandHelp = $"{Year}\n{Day}\n{Title}\n{Language}";
}

public static class HelpCommandConstants
{
    public static readonly Command Help = new()
    {
        Name = "help",
        Shortcut = "h",
        AcceptedType = typeof(string),
        Description = "List of available commands, usage, descriptions, and accepted types."
    };
}
namespace AdventOfCode.Services.Generate;

public class GeneratingOrchestrator(GeneratorContext context)
{
    private readonly GeneratorContext _context = context;

    public void Generate()
    {
        Console.WriteLine($"Generating files to solve puzzle in {_context.Language} for day {_context.DisplayDay} of {_context.Year}");

        if (!TryGetGenerator(out var puzzleGenerator))
            throw new InvalidOperationException($"Invalid language {_context.Language}.");

        puzzleGenerator.GenerateTemplates();

        Console.WriteLine($"Finished generating files for day {_context.DisplayDay} of {_context.Year}");
    }

    private bool TryGetGenerator(out BasePuzzleGenerator puzzleGenerator)
    {
        switch(_context.Language)
        {
            case Language.CSharp:
                puzzleGenerator = new CSharpPuzzleGenerator(_context);
                return true;
            case Language.Python:
                puzzleGenerator = new PythonPuzzleGenerator(_context);
                return true;
            default:
                puzzleGenerator = null;
                return false;
        }
    }
}
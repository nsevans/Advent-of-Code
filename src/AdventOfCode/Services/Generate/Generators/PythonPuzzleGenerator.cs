namespace AdventOfCode.Services.Generate.Generators;

public class PythonPuzzleGenerator(GeneratorContext context) : BasePuzzleGenerator(context)
{
    protected override string TemplateFolderName => "python";

    protected override bool GenerateTemplatesInternal()
    {
        if (!TryLoadTemplate($"{Enum.GetName(TemplateType.Day)}.cs.template", out var cSharpDayTemplate)
            || !TryLoadTemplate($"{Enum.GetName(TemplateType.Part)}.cs.template", out var cSharpPartTemplate)
            || !TryLoadTemplate($"{Enum.GetName(TemplateType.Part)}.py.template", out var pythonPartTemplate))
        {
            return false;
        }

        var cSharpDayClass = PopulateFile(cSharpDayTemplate);

        var cSharpPartClasses = new string[]
        {
            PopulateFile(cSharpPartTemplate, 1),
            PopulateFile(cSharpPartTemplate, 2)
        };

        var pythonPartScripts = new string[]
        {
            PopulateFile(pythonPartTemplate, 1),
            PopulateFile(pythonPartTemplate, 2)
        };

        try
        {
            File.WriteAllText($"{OutputDirectory}/Day_{_context.DisplayDay}.cs", cSharpDayClass);

            foreach (var (index, partClass) in cSharpPartClasses.Select((t, i) => (i, t)))
            {
                File.WriteAllText($"{OutputDirectory}/Part_{(index + 1).PadLeft(2, '0')}.cs", partClass);
            }

            foreach (var (index, partScript) in pythonPartScripts.Select((t, i) => (i, t)))
            {
                File.WriteAllText($"{OutputDirectory}/Part_{(index + 1).PadLeft(2, '0')}.py", partScript);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Failed to write to files.\n{ex.Message}");
            return false;
        }

        return true;
    }
}
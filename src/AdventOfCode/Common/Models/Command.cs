namespace AdventOfCode.Common.Models;

public class Command
{
    public string Name { get; set; }

    public string Shortcut { get; set; }

    public string[] Options => [$"-{Shortcut}", $"--{Name}"];

    public Type AcceptedType { get; set; }

    public string[] AcceptedValues { get; set; } = [];

    public string Description { get; set; }

    public string ExampleText {
        get
        {
            var builder = new StringBuilder($"--{Name}");

            if (AcceptedType != null)
            {
                builder.Append($" <{AcceptedType.Name}");

                if (AcceptedValues.Length > 0)
                    builder.Append($" [{string.Join(", ", AcceptedValues.Select(t => $"\"{t}\""))}]");

                builder.Append('>');
            }
            return builder.ToString();
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder($"-{Shortcut} | --{Name}");

        if (!string.IsNullOrWhiteSpace(Description))
            builder.Append($": {Description}");

        builder.Append('\n');

        if (AcceptedType != null)
            builder.Append($"\tAccepted Type: {AcceptedType.Name}\n");

        if (AcceptedValues.Length > 0)
            builder.Append($"\tAccepted Values: {string.Join(", ", AcceptedValues.Select(t => $"\"{t}\""))}\n");

        if (!string.IsNullOrWhiteSpace(ExampleText))
            builder.Append($"\tExample: {ExampleText}");

        return builder.ToString();
    }
}
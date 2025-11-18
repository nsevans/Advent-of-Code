using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Services;

namespace AdventOfCode.Services.Help;

public class HelpArgumentHandler : IInputHandler<string>
{
    public static string HandleInput(string[] args)
    {
        args.TryGetArgumentAtIndex(1, out var command);

        if (command != null && !command.StartsWith("--"))
            command = $"--{command}";

        return command;
    }
}
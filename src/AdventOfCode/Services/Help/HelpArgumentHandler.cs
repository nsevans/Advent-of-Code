namespace AdventOfCode.Services.Help;

public class HelpArgumentHandler : IInputHandler<string>
{
    public static string HandleInput(string[] args)
    {
        args.TryGetArgumentAtIndex(1, out var command);

        return command;
    }
}
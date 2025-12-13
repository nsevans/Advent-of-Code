namespace AdventOfCode.Common.Extensions;

public static class NumberExtensions
{
    public static string PadLeft<T>(this T value, int totalWidth) where T : struct, IComparable<T>
        => value.ToString().PadLeft(totalWidth);

    public static string PadLeft<T>(this T value, int totalWidth, char paddingCharacter) where T : struct, IComparable<T>
        => value.ToString().PadLeft(totalWidth, paddingCharacter);

    public static string PadRight<T>(this T value, int totalWidth) where T : struct, IComparable<T>
        => value.ToString().PadRight(totalWidth);

    public static string PadRight<T>(this T value, int totalWidth, char paddingCharacter) where T : struct, IComparable<T>
        => value.ToString().PadRight(totalWidth, paddingCharacter);
}
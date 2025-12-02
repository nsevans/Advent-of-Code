using System;
using System.Linq;
using AdventOfCode.Common.Models;

namespace AdventOfCode.Common.Extensions;

public static class ArgumentExtensions
{
    /// <summary>
    /// Get the value of the corresponding argument
    /// </summary>
    /// <param name="inputArgs">The list of inputed arguments</param>
    /// <param name="arg">The requested argument</param>
    /// <param name="isRequired">Flag that determines if the requested argument is required. If it is required and not present an exception is thrown</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetValueForArgument(this string[] inputArgs, string arg, bool isRequired = false)
    {
        var argIndex = Array.IndexOf(inputArgs, arg);

        // Argument not found
        if (argIndex == -1)
        {
            if (isRequired)
                throw new ArgumentException($"Argument '{arg}' is a required argument.");
            return null;
        }

        // Argument value not given
        if (argIndex + 1 >= inputArgs.Length)
            throw new ArgumentException($"Argument '{arg}' requires a corresponding value.");

        return inputArgs[argIndex + 1];
    }

    /// <summary>
    /// Get the first value of the corresponding list of argument options.
    /// Used for scenarios where multiple arguments are for the same value, i.e.: "--year" and "-y"
    /// </summary>
    /// <param name="inputArgs">The list of inputed arguments</param>
    /// <param name="argOptions">The list of requested arguments</param>
    /// <param name="isRequired">Flag that determines if the requested argument is required. If it is required and not present an exception is thrown</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetValueForArgument(this string[] inputArgs, string[] argOptions, bool isRequired = false, string[] allowedValues = null)
    {
        if (!argOptions.Any(a => inputArgs.Contains(a)))
        {
            if (isRequired)
                throw new ArgumentException($"Argument '{argOptions[1]}' is a required argument.");
            return null;
        }

        if (argOptions.Intersect(inputArgs).Count() >= 2)
            throw new ArgumentException($"Cannot specify the same arguments twice: [{string.Join(", ", argOptions)}]");

        var value = (string)null;

        foreach (var argOption in argOptions)
        {
            var argIndex = Array.IndexOf(inputArgs, argOption);

            // Argument not found, try next
            if (argIndex == -1)
                continue;

            // Argument value not given
            if (argIndex + 1 >= inputArgs.Length)
                throw new ArgumentException($"Argument '{argOption}' requires a corresponding value.");

            value = inputArgs[argIndex + 1];
            break;
        }

        if (allowedValues != null && !allowedValues.Contains(value))
            throw new ArgumentException($"Argument '{argOptions}' passed invalid value '{value}'.");

        return value;
    }

	/// <summary>
	/// Check if the given argument exists
	/// </summary>
	/// <param name="inputArgs">The list of arguments</param>
	/// <param name="arg">The requested argument</param>
	/// <returns></returns>
	public static bool HasArgument(this string[] inputArgs, string arg) => inputArgs.Contains(arg);

    /// <summary>
    /// Check if any of the given arguments exists
    /// </summary>
    /// <param name="inputArgs">The list of arguments</param>
    /// <param name="argOptions">The list of requested arguments</param>
    /// <returns></returns>
    public static bool HasArgument(this string[] inputArgs, string[] argOptions) => argOptions.Any(a => inputArgs.Contains(a));

    /// <summary>
    /// Try to get the argument from the argument list at the given index.
    /// </summary>
    /// <param name="inputArgs">The list of inputed arguments</param>
    /// <param name="index">The index of the desired inputed argument</param>
    /// <param name="result">The value at the given index (null if none found)</param>
    /// <returns></returns>
    public static bool TryGetArgumentAtIndex(this string[] inputArgs, int index, out string result)
    {
        result = null;
        if (index < 0 || index >= inputArgs.Length)
            return false;

        if (string.IsNullOrWhiteSpace(inputArgs[index]))
            return false;

        result = inputArgs[index];
        return true;
    }
}
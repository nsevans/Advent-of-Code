namespace AdventOfCode.Common.Services;

public interface IInputHandler<T>
{
	public static abstract T HandleInput(string[] args);
}
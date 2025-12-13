namespace AdventOfCode.Common.Interfaces;

public interface IInputHandler<T>
{
	public static abstract T HandleInput(string[] args);
}
namespace AdventOfCode.Common.Models;

public class Point2D(int x, int y)
{
	public readonly int X = x;
	public readonly int Y = y;

    public override bool Equals(object other)
    {
		return Equals(other);
    }

	public bool Equals(Point2D other)
	{
		return other != null && X == other.X && Y == other.Y;
	}

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
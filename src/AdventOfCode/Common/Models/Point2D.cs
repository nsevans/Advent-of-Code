namespace AdventOfCode.Common.Models;

public class Point2D
{
	public readonly int X;
    public readonly int Y;

    public Point2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point2D((int x, int y) position)
    {
        X = position.x;
        Y = position.y;
    }

    public (int x, int y) ToTuple() => (X, Y);

    public override string ToString() => $"[{X}, {Y}]";

    public static Point2D operator +(Point2D left, Point2D right) => new(left.X + right.X, left.Y + right.Y);

    public static Point2D operator +(Point2D left, (int x, int y) right) => new(left.X + right.x, left.Y + right.y);

    public static Point2D operator -(Point2D left, Point2D right) => new(left.X - right.X, left.Y - right.Y);

    public static Point2D operator -(Point2D left, (int x, int y) right) => new(left.X - right.x, left.Y - right.y);

    public static bool operator ==(Point2D left, Point2D right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null)
            return false;
        if (right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Point2D left, Point2D right) => !(left == right);

	public bool Equals(Point2D other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

		return X == other.X && Y == other.Y;
	}

    public override bool Equals(object other) => Equals(other as Point2D);

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
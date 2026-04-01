namespace LifeSim;

public readonly record struct Point2(int X, int Y)
{
    public Point2 Offset(int dx, int dy) => new(X + dx, Y + dy);

    public Point2 Wrap(int width, int height)
    {
        var x = ((X % width) + width) % width;
        var y = ((Y % height) + height) % height;
        return new Point2(x, y);
    }

    public int ToroidalDistanceTo(Point2 other, int width, int height)
    {
        var dx = ToroidalAxisDistance(X, other.X, width);
        var dy = ToroidalAxisDistance(Y, other.Y, height);
        return dx + dy;
    }

    public bool IsNeighborOrSame(Point2 other) =>
        System.Math.Abs(X - other.X) <= 1 && System.Math.Abs(Y - other.Y) <= 1;

    public override string ToString() => $"({X},{Y})";

    private static int ToroidalAxisDistance(int a, int b, int size)
    {
        var diff = System.Math.Abs(a - b);
        return System.Math.Min(diff, size - diff);
    }
}
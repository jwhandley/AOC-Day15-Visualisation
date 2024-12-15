using Raylib_cs;

namespace AdventOfCode_Day15;

public static class RectExtensions
{
    public static bool Intersects(this Rectangle rect, Rectangle other)
    {
        if (rect.X + rect.Width <= other.X) return false;
        if (rect.Y + rect.Height <= other.Y) return false;
        if (other.X + other.Width <= rect.X) return false;
        if (other.Y + other.Height <= rect.Y) return false;
        return true;
    }
}
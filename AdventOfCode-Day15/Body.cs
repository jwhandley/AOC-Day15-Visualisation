using System.Numerics;
using Raylib_cs;

namespace AdventOfCode_Day15;

public abstract class Body(Rectangle rect, Color c)
{
    public Rectangle Rect = rect;
    protected Color Color = c;

    public override string ToString() => $"{Rect} {GetHashCode()}";

    public void Move(Vector2 dir, List<Body> bodies)
    {
        Rect = Rect with { Position = Rect.Position + dir };

        foreach (var body in bodies.Where(b => b != this && b.Rect.Intersects(Rect)))
        {
            body.Move(dir, bodies);
        }
    }

    public bool CanMove(Vector2 dir, HashSet<Rectangle> walls, List<Body> bodies)
    {
        var newRect = Rect with { Position = Rect.Position + dir };
        if (walls.Any(w => w.Intersects(newRect))) return false;

        foreach (var body in bodies.Where(b => b.Rect.Intersects(newRect) && b != this))
        {
            if (!body.CanMove(dir, walls, bodies)) return false;
        }

        return true;
    }

    public abstract void Draw(float scale, Vector2 offset);
}
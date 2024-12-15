using System.Numerics;
using Raylib_cs;

namespace AdventOfCode_Day15;

public class Robot(Rectangle rect) : Body(rect, Color.Red)
{
    public override void Draw(float scale, Vector2 offset)
    {
        Raylib.DrawRectangleV(Rect.Position * scale + offset, Rect.Size * scale, Color);
    }
}
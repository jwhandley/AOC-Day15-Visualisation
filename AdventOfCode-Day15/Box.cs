using System.Numerics;
using Raylib_cs;

namespace AdventOfCode_Day15;

public class Box(Rectangle rect) : Body(rect, Color.Gray)
{
    public override void Draw(float scale, Vector2 offset)
    {
        var scaledRect = new Rectangle(Rect.Position * scale + offset, Rect.Width * scale, Rect.Height * scale);
        Raylib.DrawRectangleLinesEx(scaledRect, 0.1f * scale, Color);
    }
}
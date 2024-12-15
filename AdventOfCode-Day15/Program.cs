using System.Numerics;
using AdventOfCode_Day15;
using Raylib_cs;


const float scale = 10;
var offset = new Vector2(100, 100);
string input = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Assets/input.txt"));
string[] parts = input.Split("\n\n");

var robot = new Robot(new Rectangle(0, 0, 0, 0));
HashSet<Rectangle> walls = [];
List<Body> boxes = [];
foreach ((int r, string line) in parts[0].Split('\n').Index())
{
    foreach ((int c, char ch) in line.Index())
    {
        if (ch == '@') robot = new Robot(new Rectangle(c * 2, r, 1, 1));
        if (ch == '#') walls.Add(new Rectangle(c * 2, r, 2, 1));
        if (ch == 'O')
        {
            var box = new Box(new Rectangle(c * 2, r, 2, 1));
            boxes.Add(box);
        }
    }
}


var up = new Vector2(0, -1);
var down = new Vector2(0, 1);
var left = new Vector2(-1, 0);
var right = new Vector2(1, 0);

Queue<Vector2> moves = [];
foreach (char c in parts[1])
{
    switch (c)
    {
        case '^':
            moves.Enqueue(up);
            break;
        case 'v':
            moves.Enqueue(down);
            break;
        case '<':
            moves.Enqueue(left);
            break;
        case '>':
            moves.Enqueue(right);
            break;
    }
}

while (moves.Count > 0)
{
    var move = moves.Dequeue();
    if (robot.CanMove(move, walls, boxes))
    {
        robot.Move(move, boxes);
    }
}

Console.WriteLine(CalculateScore(boxes));
return;

Raylib.InitWindow(1200, 800, "Day 15");
Raylib.SetTargetFPS(60);
Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);



while (!Raylib.WindowShouldClose())
{
    if (moves.Count > 0)
    {
        var move = moves.Dequeue();
        if (robot.CanMove(move, walls, boxes))
        {
            robot.Move(move, boxes);
        }
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.RayWhite);
    
    foreach (var wall in walls)
    {
        Raylib.DrawRectangleV(wall.Position * scale + offset, wall.Size * scale, Color.Black);
    }

    robot.Draw(scale, offset);
    foreach (var box in boxes)
    {
        box.Draw(scale, offset);
    }

    Raylib.DrawText($"Remaining moves: {moves.Count}", 20, 20, 20, Color.Black);
    Raylib.DrawText($"Score: {CalculateScore(boxes)}", 20, 40, 20, Color.Black);
    Raylib.EndDrawing();
}

Raylib.CloseWindow();

static long CalculateScore(IEnumerable<Body> boxes) => boxes.Aggregate<Body, long>(0,
    (current, box) => current + ((int)box.Rect.Position.Y * 100 + (int)box.Rect.Position.X));




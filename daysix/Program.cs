var lines = File.ReadAllLines("input.txt");

const char guardSymbol = '^';
const char emptySymbol = '.';
const char visitedSymbol = 'X';

char[,] map = new char[lines[0].Length, lines.Length];

FillMap(lines, map);

var position = FindGuardPosition(map);


if (!PositionIsValid(map, position)) throw new Exception("Guard not found");

var guard = new Guard { Position = position, Direction = new(0, -1) };

Position nextPosition = Position.Invalid;

do
{
    SetPosition(map, guard.Position, visitedSymbol);
    nextPosition = MoveGuard(map, guard);
} while (PositionIsValid(map, nextPosition));

PrintMap(map);

var visited = CountVisited(map);

Console.WriteLine($"Guard visited {visited}");

Position MoveGuard(char[,] map, Guard guard)
{
    var nextPosition = new Position(guard.Position.x + guard.Direction.x, guard.Position.y + guard.Direction.y);

    if (!PositionIsValid(map, nextPosition)) return Position.Invalid;

    if (!PositionIsFree(map, nextPosition))
    {
        guard.Direction = new Direction(-guard.Direction.y, guard.Direction.x);
        return guard.Position;
    }

    guard.Position = nextPosition;

    return nextPosition;
}

int CountVisited(char[,] map)
{
    int count = 0;
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y] == visitedSymbol)
            {
                count++;
            }
        }
    }
    return count;
}

void PrintMap(char[,] map)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            Console.Write(map[x, y]);
        }
        Console.WriteLine();
    }
}

void SetPosition(char[,] map, Position position, char value) => map[position.x, position.y] = value;

bool PositionIsFree(char[,] map, Position position) => map[position.x, position.y] == emptySymbol || map[position.x, position.y] == visitedSymbol;

bool PositionIsValid(char[,] map, Position position) => position.x >= 0 && position.x < map.GetLength(0) && position.y >= 0 && position.y < map.GetLength(1);

Position FindGuardPosition(char[,] map)
{
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y] == guardSymbol)
            {
                return new(x, y);
            }
        }
    }
    return Position.Invalid;
}

static void FillMap(string[] lines, char[,] map)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        for (int y = 0; y < lines.Length; y++)
        {
            map[x, y] = lines[y][x];
        }
    }
}

class Guard
{
    public required Position Position { get; set; }
    public required Direction Direction { get; set; }
}

record Direction(int x, int y);

record Position(int x, int y)
{
    public static Position Invalid => new(-1, -1);
}
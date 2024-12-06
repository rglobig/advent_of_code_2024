using CommunityToolkit.HighPerformance;

static class Helper
{
    public static readonly char GuardSymbol = '^';
    public static readonly char EmptySymbol = '.';
    public static readonly char VisitedSymbol = 'X';
    public static readonly char BlockingSymbol = '#';

    public static Position MoveGuard(ReadOnlyMemory2D<char> map, Guard guard)
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

    public static int CountVisited(ReadOnlyMemory2D<char> map, HashSet<Position> visited)
    {
        int count = 0;
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (map.Span[x, y] == VisitedSymbol)
                {
                    count++;
                    visited.Add(new Position(x, y));
                }
            }
        }
        return count;
    }

    public static void PrintMap(ReadOnlyMemory2D<char> map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                Console.Write(map.Span[x, y]);
            }
            Console.WriteLine();
        }
    }

    public static void SetPosition(char[,] map, Position position, char value) => map[position.x, position.y] = value;

    public static bool PositionIsFree(ReadOnlyMemory2D<char> map, Position position) => map.Span[position.x, position.y] == EmptySymbol || map.Span[position.x, position.y] == VisitedSymbol;

    public static bool PositionIsValid(ReadOnlyMemory2D<char> map, Position position) => position.x >= 0 && position.x < map.Width && position.y >= 0 && position.y < map.Width;

    public static Position FindGuardPosition(ReadOnlyMemory2D<char> map)
    {
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (map.Span[x, y] == GuardSymbol)
                {
                    return new(x, y);
                }
            }
        }
        return Position.Invalid;
    }

    public static ReadOnlyMemory2D<char> FillMap(string[] lines, char[,] map)
    {
        for (int x = 0; x < lines[0].Length; x++)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                map[x, y] = lines[y][x];
            }
        }
        return new ReadOnlyMemory2D<char>(map);
    }
}

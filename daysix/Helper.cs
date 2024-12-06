static class Helper
{
    public static readonly char GuardSymbol = '^';
    public static readonly char EmptySymbol = '.';
    public static readonly char VisitedSymbol = 'X';

    public static Position MoveGuard(char[,] map, Guard guard)
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

    public static int CountVisited(char[,] map)
    {
        int count = 0;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == VisitedSymbol)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public static void PrintMap(char[,] map)
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

    public static void SetPosition(char[,] map, Position position, char value) => map[position.x, position.y] = value;

    public static bool PositionIsFree(char[,] map, Position position) => map[position.x, position.y] == EmptySymbol || map[position.x, position.y] == VisitedSymbol;

    public static bool PositionIsValid(char[,] map, Position position) => position.x >= 0 && position.x < map.GetLength(0) && position.y >= 0 && position.y < map.GetLength(1);

    public static Position FindGuardPosition(char[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == GuardSymbol)
                {
                    return new(x, y);
                }
            }
        }
        return Position.Invalid;
    }

    public static void FillMap(string[] lines, char[,] map)
    {
        for (int x = 0; x < lines[0].Length; x++)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                map[x, y] = lines[y][x];
            }
        }
    }
}

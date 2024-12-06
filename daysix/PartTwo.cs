using static Helper;

static class PartTwo
{
    public static void CalculateAndPrint(string[] lines)
    {
        Console.WriteLine("====== PART TWO ======");
        int loops = 0;

        char[,] map = new char[lines[0].Length, lines.Length];

        FillMap(lines, map);

        var startPosition = FindGuardPosition(map);

        if (!PositionIsValid(map, startPosition)) throw new Exception("Guard not found");

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == GuardSymbol) continue;
                if (map[x, y] == BlockingSymbol) continue;
                
                map[x, y] = BlockingSymbol;

                var found = FindLoop(map, startPosition);
                if(found) loops++;

                map[x, y] = EmptySymbol;
            }
        }

        Console.WriteLine($"Found Loops: {loops}");
        Console.WriteLine("======================");
    }

    static bool FindLoop(char[,] map, Position startPosition)
    {
        var guard = new Guard { Position = startPosition, Direction = new(0, -1) };

        HashSet<(Position, Direction)> visited = [];

        Position nextPosition;

        do
        {
            var current = (guard.Position, guard.Direction);
            if (visited.Contains(current)) return true;

            visited.Add(current);

            SetPosition(map, guard.Position, VisitedSymbol);
            nextPosition = MoveGuard(map, guard);
        } while (PositionIsValid(map, nextPosition));

        return false;
    }
}

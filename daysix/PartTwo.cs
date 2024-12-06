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

        object lockObject = new();
        Parallel.For(0, map.GetLength(0), x =>
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == GuardSymbol || map[x, y] == BlockingSymbol) continue;

                var mapCopy = (char[,])map.Clone();
                mapCopy[x, y] = BlockingSymbol;
                var found = FindLoop(mapCopy, startPosition);

                if (found)
                {
                    lock (lockObject)
                    {
                        loops++;
                    }
                }
            }
        });

        Console.WriteLine($"Found Loops: {loops}");
        Console.WriteLine("======================");
    }

    static bool FindLoop(char[,] map, Position startPosition)
    {
        var guard = new Guard { Position = startPosition, Direction = new(0, -1) };

        HashSet<(Position, Direction)> visited = new();

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
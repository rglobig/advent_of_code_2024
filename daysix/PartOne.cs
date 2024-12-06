using static Helper;

static class PartOne
{
    public static HashSet<Position> CalculateAndPrint(string[] lines)
    {
        Console.WriteLine("====== PART ONE ======");

        char[,] map = new char[lines[0].Length, lines.Length];

        FillMap(lines, map);

        var position = FindGuardPosition(map);

        if (!PositionIsValid(map, position)) throw new Exception("Guard not found");

        var guard = new Guard { Position = position, Direction = new(0, -1) };

        Position nextPosition;

        do
        {
            SetPosition(map, guard.Position, VisitedSymbol);
            nextPosition = MoveGuard(map, guard);
        } while (PositionIsValid(map, nextPosition));

        HashSet<Position> visitedMap = new();

        var visited = CountVisited(map, visitedMap);

        Console.WriteLine($"Guard visited {visited} distinct positions");
        Console.WriteLine("======================");

        return visitedMap;
    }
}
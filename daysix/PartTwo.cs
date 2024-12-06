using CommunityToolkit.HighPerformance;
using static Helper;

static class PartTwo
{
    public static void CalculateAndPrint(string[] lines)
    {
        Console.WriteLine("====== PART TWO ======");
        int loops = 0;

        var map = FillMap(lines, new char[lines[0].Length, lines.Length]);

        var startPosition = FindGuardPosition(map);

        if (!PositionIsValid(map, startPosition)) throw new Exception("Guard not found");

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 4
        };

        Parallel.For(0, map.Width, parallelOptions, x =>
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (map.Span[x, y] == GuardSymbol || map.Span[x, y] == BlockingSymbol) continue;

                var found = FindLoop(map, startPosition, new(x, y));

                if (found) Interlocked.Increment(ref loops);
            }
        });

        Console.WriteLine($"Found Loops: {loops}");
        Console.WriteLine("======================");
    }

    static bool FindLoop(ReadOnlyMemory2D<char> map, Position startPosition, Position blockPosition)
    {
        var guard = new Guard { Position = startPosition, Direction = new(0, -1) };

        HashSet<(Position, Direction)> visited = new();

        Position nextPosition;

        do
        {
            var current = (guard.Position, guard.Direction);
            if (visited.Contains(current)) return true;

            visited.Add(current);

            nextPosition = MoveGuard(map, guard, blockPosition);
        } while (PositionIsValid(map, nextPosition));

        return false;
    }

    public static Position MoveGuard(ReadOnlyMemory2D<char> map, Guard guard, Position blockPosition)
    {
        var nextPosition = new Position(guard.Position.x + guard.Direction.x, guard.Position.y + guard.Direction.y);

        if (!PositionIsValid(map, nextPosition)) return Position.Invalid;

        if (!PositionIsFree(map, nextPosition) || nextPosition == blockPosition)
        {
            guard.Direction = new Direction(-guard.Direction.y, guard.Direction.x);
            return guard.Position;
        }

        guard.Position = nextPosition;

        return nextPosition;
    }
}
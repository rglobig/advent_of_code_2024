using static System.Console;

class PartTwo
{
    public void CalculateAndPrint(string[] lines)
    {
        WriteLine("====== PART TWO ======");

        char[,] map = new char[lines[0].Length, lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                map[x, y] = line[x];
            }
        }

        var crossSum = 0;

        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                var crossFound = CheckCross(map, x, y);
                if(!crossFound) continue;

                WriteLine($"Found cross at {x}, {y}");
                crossSum++;
            }
        }
        WriteLine($"Found {crossSum} crosses");
        WriteLine("======================");
    }

    private bool CheckCross(char[,] map, int x, int y)
    {
        var middle = GetChar(map, x, y);

        if(middle != 'A') return false;

        var topLeft = GetChar(map, x - 1, y - 1);
        var topRight = GetChar(map, x + 1, y - 1);
        var bottomLeft = GetChar(map, x - 1, y + 1);
        var bottomRight = GetChar(map, x + 1, y + 1);

        if (topLeft == 'M' && topRight == 'S' && bottomLeft == 'M' && bottomRight == 'S') return true;
        if (topLeft == 'S' && topRight == 'S' && bottomLeft == 'M' && bottomRight == 'M') return true;
        if (topLeft == 'M' && topRight == 'M' && bottomLeft == 'S' && bottomRight == 'S') return true;
        if (topLeft == 'S' && topRight == 'M' && bottomLeft == 'S' && bottomRight == 'M') return true;

        return false;
    }

    char GetChar(char[,] map, int x, int y)
    {
        if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
        {
            return '.';
        }
        return map[x, y];
    }
}

var lines = File.ReadAllLines("input.txt");

var visited = PartOne.CalculateAndPrint(lines);
PartTwo.CalculateAndPrint(lines, visited);

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
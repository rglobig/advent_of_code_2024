var lines = File.ReadAllLines("input.txt");

PartOne.CalculateAndPrint(lines);
PartTwo.CalculateAndPrint(lines);

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
using System.Collections.ObjectModel;

var lines = File.ReadAllLines("input.txt");

var grid = new Grid(lines[0].Length, lines.Length);

var antennaGroups = ParseDataIntoAntennaGroups(lines);
var antennaCombinations = CreateAntennaCombinations(antennaGroups);
var antinodes = CreateAntinodesFromAntennaCombinations(antennaCombinations, grid);

Console.WriteLine($"There are {antinodes.Count} Antinodes on the Grid.");

bool IsAntinodeInGrid(Antinode antinode, Grid grid)
{
    return antinode.Position.X >= 0 && antinode.Position.X < grid.Width && antinode.Position.Y >= 0 && antinode.Position.Y < grid.Height;
}

Dictionary<Vector2, Antinode> CreateAntinodesFromAntennaCombinations(ReadOnlyDictionary<char, List<AntennaCombination>> antennaCombinations, Grid grid)
{
    Dictionary<Vector2, Antinode> antinodes = [];

    foreach(var group in antennaCombinations)
    {
        foreach (var combination in group.Value)
        {
            var (a, b) = CalculateAntinodes(combination);
            if (IsAntinodeInGrid(a, grid)) antinodes[a.Position] = a;
            if (IsAntinodeInGrid(b, grid)) antinodes[b.Position] = b;
        }
    }

    return antinodes;
}

(Antinode a, Antinode b) CalculateAntinodes(AntennaCombination combination)
{
    var antennaA = combination.A;
    var antennaB = combination.B;

    var delta = new Vector2(antennaB.Position.X - antennaA.Position.X, antennaB.Position.Y - antennaA.Position.Y);

    var antinodeAPosition = new Vector2(antennaA.Position.X - delta.X, antennaA.Position.Y - delta.Y);
    var antinodeBPosition = new Vector2(antennaB.Position.X + delta.X, antennaB.Position.Y + delta.Y);

    var antinodeA = new Antinode(antinodeAPosition);
    var antinodeB = new Antinode(antinodeBPosition);

    return (antinodeA, antinodeB);
}

static ReadOnlyDictionary<char, List<Antenna>> ParseDataIntoAntennaGroups(string[] lines)
{
    Dictionary<char, List<Antenna>> antennaGroups = [];
    int id = 0;
    for (int i = 0; i < lines.Length; i++)
    {
        var line = lines[i];
        for (int j = 0; j < line.Length; j++)
        {
            var c = line[j];
            if (c == '.') continue;

            if (!antennaGroups.ContainsKey(c)) antennaGroups[c] = [];
            antennaGroups[c].Add(new Antenna(id, c, new Vector2(j, i)));
            id++;
        }
    }
    return antennaGroups.AsReadOnly();
}

static ReadOnlyDictionary<char, List<AntennaCombination>> CreateAntennaCombinations(ReadOnlyDictionary<char, List<Antenna>> antennaGroups)
{
    Dictionary<char, List<AntennaCombination>> antennaCombinations = [];
    foreach (var group in antennaGroups)
    {
        for (var i = 0; i < group.Value.Count; i++)
        {
            for (var j = i + 1; j < group.Value.Count; j++)
            {
                var antennaA = group.Value.ElementAt(i);
                var antennaB = group.Value.ElementAt(j);
                var combination = new AntennaCombination(antennaA, antennaB);
                if (!antennaCombinations.ContainsKey(group.Key)) antennaCombinations[group.Key] = [];
                antennaCombinations[group.Key].Add(combination);
            }
        }
    }
    return antennaCombinations.AsReadOnly();
}

record Grid(int Width, int Height);
record Antinode(Vector2 Position);
record AntennaCombination(Antenna A, Antenna B);
record Antenna(int Id, char Type, Vector2 Position);
record Vector2(int X, int Y);
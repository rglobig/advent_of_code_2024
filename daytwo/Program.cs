var lines = File.ReadAllLines("input.txt");

var reports = new int[lines.Length][];

for (int i = 0; i < lines.Length; i++)
{
    var levels = lines[i].Split(' ');
    reports[i] = new int[levels.Length];
    for (int j = 0; j < levels.Length; j++)
    {
        reports[i][j] = int.Parse(levels[j]);
    }
}

new PartOne(reports).CalculateAndPrint();
new PartTwo(reports).CalculateAndPrint();
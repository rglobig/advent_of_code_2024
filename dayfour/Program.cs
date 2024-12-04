using System.Text;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines("input.txt");

WriteLine($"Wide: {lines[0].Length} and Height {lines.Length}");

var horizontal = GetHorizontalCount(lines, GetAllXmas);
WriteLine($"Found {horizontal} in {nameof(GetHorizontalCount)}");

var vertical = GetVerticalCount(lines, GetAllXmas);
WriteLine($"Found {vertical} in {nameof(GetVerticalCount)}");

var diagonalRight = GetDiagonalCountRight(lines, GetAllXmas);
WriteLine($"Found {diagonalRight} in {nameof(GetDiagonalCountRight)}");

var diagonalLeft = GetDiagonalCountLeft(lines, GetAllXmas);
WriteLine($"Found {diagonalLeft} in {nameof(GetDiagonalCountLeft)}");

var sum = horizontal + vertical + diagonalRight + diagonalLeft;

WriteLine($"Total sum is {sum}");

static int GetHorizontalCount(string[] lines, Func<string, int> check) => GetLinesSumRegularAndReversed(lines, check);

static int GetVerticalCount(string[] lines, Func<string, int> check)
{
    List<string> verticalLines = new();
    var length = lines[0].Length;

    for (var width = 0; width < lines[0].Length; width++)
    {
        StringBuilder builder = new();
        for (var height = 0; height < lines.Length; height++)
        {
            var line = lines[height];
            var character = line[width];
            builder.Append(character);    
        }
        verticalLines.Add(builder.ToString());
    }

    var sum = GetLinesSumRegularAndReversed(verticalLines.ToArray(), check);

    return sum;
}

static int GetDiagonalCountRight(string[] lines, Func<string, int> check)
{
    List<string> diagonals = [];
    int rows = lines.Length;
    int cols = lines[0].Length;

    for (int col = 0; col < cols; col++)
    {
        StringBuilder diagonal = new();
        for (int row = 0, c = col; row < rows && c < cols; row++, c++)
        {
            diagonal.Append(lines[row][c]);
        }
        diagonals.Add(diagonal.ToString());
    }

    for (int row = 1; row < rows; row++)
    {
        StringBuilder diagonal = new();
        for (int r = row, col = 0; r < rows && col < cols; r++, col++)
        {
            diagonal.Append(lines[r][col]);
        }
        diagonals.Add(diagonal.ToString());
    }

    return GetLinesSumRegularAndReversed(diagonals.ToArray(), check);
}

static int GetDiagonalCountLeft(string[] lines, Func<string, int> check)
{
    List<string> diagonals = [];
    int rows = lines.Length;
    int cols = lines[0].Length;

    for (int col = cols - 1; col >= 0; col--)
    {
        StringBuilder diagonal = new();
        for (int row = 0, c = col; row < rows && c >= 0; row++, c--)
        {
            diagonal.Append(lines[row][c]);
        }
        diagonals.Add(diagonal.ToString());
    }

    for (int row = 1; row < rows; row++)
    {
        StringBuilder diagonal = new();
        for (int r = row, col = cols - 1; r < rows && col >= 0; r++, col--)
        {
            diagonal.Append(lines[r][col]);
        }
        diagonals.Add(diagonal.ToString());
    }

    return GetLinesSumRegularAndReversed(diagonals.ToArray(), check);
}


static int GetLinesSumRegularAndReversed(string[] lines, Func<string, int> check)
{
    var sum = 0;
    foreach (var line in lines)
    {
        sum += check(line);
        var reversed = new string(line.Reverse().ToArray());
        sum += check(reversed!);
    }
    return sum;
}

static int GetAllXmas(string line) => Regex.Matches(line, "XMAS").Count;
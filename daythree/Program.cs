using System.Text.RegularExpressions;
using static System.Console;

var input = File.ReadAllText("input.txt");

var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

var matches = Regex.Matches(input, pattern);

int sum = 0;

for(int i = 0; i < matches.Count; i++)
{
    var match = matches[i];
    var x = int.Parse(match.Groups[1].Value);
    var y = int.Parse(match.Groups[2].Value);
    WriteLine("Found: " + match.Value);
    var result = x * y;
    WriteLine("Result: " + result);
    sum += result;
}
WriteLine("===");
WriteLine("Sum: " + sum);
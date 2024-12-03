using System.Text.RegularExpressions;
using static System.Console;

var input = File.ReadAllText("input.txt");

var mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

var matches = Regex.Matches(input, mulPattern);

int sum = 0;

SortedDictionary<int, Command> commands = [];

for (int i = 0; i < matches.Count; i++)
{
    var match = matches[i];
    var x = int.Parse(match.Groups[1].Value);
    var y = int.Parse(match.Groups[2].Value);
    WriteLine("Found: " + match.Value);
    commands.Add(match.Index, new MultiplyCommand(x, y));
    var result = x * y;
    WriteLine("Result: " + result);
    sum += result;
}
WriteLine("===");
WriteLine("Sum: " + sum);
WriteLine("===");

WriteLine("");
WriteLine("");

var doPattern = @"do\(\)";
var dontPattern = @"don't\(\)";

var doMatches = Regex.Matches(input, doPattern);
var dontMatches = Regex.Matches(input, dontPattern);

for (int i = 0; i < doMatches.Count; i++)
{
    var match = doMatches[i];
    commands.Add(match.Index, new DoCommand());
}

for (int i = 0; i < dontMatches.Count; i++)
{
    var match = dontMatches[i];
    commands.Add(match.Index, new DontCommand());
}

Command last = new DoCommand();

int secondSum = 0;

foreach (var match in commands)
{
    var command = match.Value;

    if (command is DoCommand || command is DontCommand DontCommand)
    {
        last = command;
        continue;
    }

    if(command is MultiplyCommand && last is DoCommand)
    {
        var mul = command as MultiplyCommand;
        var result = mul.x * mul.y;
        WriteLine("Command is active: " + mul);
        WriteLine("Result: " + result);
        secondSum += result;
    }
}
WriteLine("===");
WriteLine("secondSum: " + secondSum);
WriteLine("===");

record Command;
record DoCommand : Command;
record DontCommand : Command;
record MultiplyCommand(int x, int y) : Command;
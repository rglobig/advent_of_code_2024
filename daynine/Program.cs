var diskMap = File.ReadAllText("input.txt");

var diskSpaces = ParseDiskMapToDiskSpace(diskMap);

diskSpaces = MoveFilesForPartOne(diskSpaces);

PrintDiskSpace(diskSpaces);

var sum = CalculateCheckSum(diskSpaces);
Console.WriteLine("Sum is:" + sum);

static void PrintDiskSpace(SortedDictionary<int, DiskSpace> diskSpaces)
{
    Console.WriteLine("After moving files to the front:");
    for (int i = 0; i < diskSpaces.Count; i++)
    {
        var disk = diskSpaces[i];
        if (disk is DiskFree) Console.ForegroundColor = ConsoleColor.Yellow;
        if (disk is DiskFile file && file.Moved) Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(disk.Type);
        Console.ResetColor();
    }
    Console.WriteLine();
}

static long CalculateCheckSum(SortedDictionary<int, DiskSpace> diskSpaces)
{
    long sum = 0;

    for (int i = 0; i < diskSpaces.Count; i++)
    {
        if (!diskSpaces.TryGetValue(i, out DiskSpace? space)) break;
        if (space is DiskFile file)
        {
            sum += (i * file.Id);
        }
    }

    return sum;
}
static SortedDictionary<int, DiskSpace> MoveFilesForPartOne(SortedDictionary<int, DiskSpace> diskSpaces)
{
    int frontIndex = 0;

    for (int i = diskSpaces.Count - 1; i >= 0; i--)
    {
        var space = diskSpaces[i];
        if (space is not DiskFile)
        {
            diskSpaces.Remove(i);
            continue;
        }

        if (frontIndex > i) break;

        for (var j = frontIndex; j < diskSpaces.Count; j++, frontIndex++)
        {
            if (j > i) break;
            var frontSpace = diskSpaces[j];
            if (frontSpace is not DiskFree) continue;
            var file = (DiskFile)space;
            diskSpaces[j] = file with { Moved = true };
            diskSpaces.Remove(i);
            break;
        }
    }

    return diskSpaces;
}

static SortedDictionary<int, DiskSpace> ParseDiskMapToDiskSpace(string diskMap)
{
    SortedDictionary<int, DiskSpace> diskSpace = [];

    DiskSpace lastSpace = new DiskFree('.');

    var diskSpaceIndex = 0;
    var fileId = -1;

    for (int i = 0; i < diskMap.Length; i++)
    {
        var type = diskMap[i];
        var size = type - '0';

        if (size == 0)
        {
            if (lastSpace is DiskFree)
                lastSpace = new DiskFile(0, '0', false);
            else if(lastSpace is DiskFile)
                lastSpace = new DiskFree('.');
            continue;
        }

        if (lastSpace is DiskFree) fileId++;

        for (var j = 0; j < size; j++)
        {
            if (lastSpace is DiskFree)
                diskSpace[diskSpaceIndex + j] = new DiskFile(fileId, type, false);
            else if (lastSpace is DiskFile)
                diskSpace[diskSpaceIndex + j] = new DiskFree('.');
        }
        diskSpaceIndex += size;
        lastSpace = diskSpace[diskSpaceIndex - 1];
    }
    return diskSpace;
}

abstract record DiskSpace(char Type);
record DiskFree(char Type) : DiskSpace(Type);
record DiskFile(int Id, char Type, bool Moved) : DiskSpace(Type);
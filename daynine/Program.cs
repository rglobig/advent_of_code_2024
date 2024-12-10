var diskMap = File.ReadAllText("example.txt");

PartOne partOne = new();
partOne.CalculateAndPrint(diskMap);

PartTwo partTwo = new();
partTwo.CalculateAndPrint(diskMap);

abstract record DiskSpace(char Type, int Size);
record DiskFree(char Type, int Size) : DiskSpace(Type, Size);
record DiskFile(int Id, char Type, bool Moved, int Size, int BlockIndex) : DiskSpace(Type, Size);
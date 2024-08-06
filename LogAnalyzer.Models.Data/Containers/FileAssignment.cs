namespace LogAnalyzer.Models.Data.Containers;

public struct FileAssignment(int fileIndex, int totalFileCount)
{
    public int FileIndex = fileIndex;
    public int TotalFileCount = totalFileCount;
}
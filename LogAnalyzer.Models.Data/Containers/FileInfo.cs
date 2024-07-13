namespace LogAnalyzer.Models.Data.Containers;

public readonly struct FileInfo(string name, string path, string fullName)
{
    public string Name { get; } = name;
    public string Path { get; } = path;
    public string FullName { get; } = fullName;
}
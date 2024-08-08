namespace LogAnalyzer.Models.Data.Containers;

public class FileInfo(int index, string name, string path, string fullName)
{
    public int FileIndex { get; } = index;
    public string Name { get; } = name;
    public string NameWithoutExtention => System.IO.Path.GetFileNameWithoutExtension(Name);
    public string Path { get; } = path;
    public string FullName { get; } = fullName;
}
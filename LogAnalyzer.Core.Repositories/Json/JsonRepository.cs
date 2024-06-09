using System.Reflection;

namespace LogAnalyzer.Core.Repositories.Json;

public class JsonRepository<T> : RepositoryBase<T>
    where T : class, ISavable
{
    private readonly string _jsonSavePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Content\Json";

    public override void Initialize()
    {
        if (!Directory.Exists(_jsonSavePath))
            Directory.CreateDirectory(_jsonSavePath);
    }

    public override IEnumerable<T> LoadAll()
    {
        return Directory.EnumerateFiles(CreateObjectPath(), "*.json")
            .Select(static filePath => JsonImporter.TryImport(filePath, out T? instance)
                ? instance
                : default)
            .Where(static instance => instance != null)!;
    }

    public override T? LoadById(long id)
    {
        JsonImporter.TryImport(CreateObjectFile(id), out T? instance);
        return instance;
    }

    public override long[] LoadIds()
    {
        IEnumerable<string> files = Directory.EnumerateFiles(CreateObjectPath(), "*.json");
        return files.Select(static file => long.Parse(Path.GetFileNameWithoutExtension(file))).ToArray();
    }

    public override void Save(T objectToSave)
    {
        JsonExporter.Export(CreateObjectFile(objectToSave.Id), objectToSave);
    }

    public override void Delete(T objectToDelete)
    {
        File.Delete(CreateObjectFile(objectToDelete.Id));
    }

    public override void DeleteAll()
    {
        IEnumerable<string> files = Directory.EnumerateFiles(CreateObjectPath(), "*.json");
        foreach (string file in files) File.Delete(file);
    }

    private string CreateObjectPath()
    {
        string path = Path.Combine(_jsonSavePath, $"{typeof(T).Name}s");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return path;
    }

    private string CreateObjectFile(long id)
    {
        return Path.Combine(CreateObjectPath(), $"{id}.json");
    }

}
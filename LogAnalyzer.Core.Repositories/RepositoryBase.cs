namespace LogAnalyzer.Core.Repositories;

public abstract class RepositoryBase<T>
    where T : class, ISavable
{
    public virtual void Initialize() {}

    public abstract IEnumerable<T> LoadAll();
    public abstract T? LoadById(long id);
    public abstract long[] LoadIds();

    public abstract void Save(T objectToSave);
    
    public abstract void Delete(T objectToDelete);
    public abstract void DeleteAll();
}
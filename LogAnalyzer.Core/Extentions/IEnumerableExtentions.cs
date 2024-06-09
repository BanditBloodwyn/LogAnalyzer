namespace LogAnalyzer.Core.Extentions;

public static class IEnumerableExtentions
{
    public static bool ContainsNone<T>(this IEnumerable<T> source, IEnumerable<T> other)
    {
        HashSet<T> set = new(other);
        return !source.Any(set.Contains);
    }
}
using LogAnalyzer.Models.Data.Containers;
using System.Collections.ObjectModel;

namespace LogAnalyzer.Models.Data;

public static class ObservableCollectionExtensions
{
    public static void AddTimeSorted(this ObservableCollection<LogEntry> collection, LogEntry item)
    {
        ArgumentNullException.ThrowIfNull(collection);

        if (collection.Count == 0 || string.Compare(item.TimeStamp, collection[^1].TimeStamp, StringComparison.Ordinal) >= 0)
        {
            collection.Add(item);
            return;
        }

        int index = collection.TakeWhile(existingItem =>
            string.Compare(existingItem.TimeStamp, item.TimeStamp, StringComparison.Ordinal) <= 0).Count();

        collection.Insert(index, item);
    }
}
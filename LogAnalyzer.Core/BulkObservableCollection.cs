using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LogAnalyzer.Core;

public class BulkObservableCollection<T> : ObservableCollection<T> where T : IHasTimestamp
{
    private bool _suppressNotification;

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (!_suppressNotification)
        {
            base.OnCollectionChanged(e);
        }
    }

    protected override void InsertItem(int index, T item)
    {
        if (!_suppressNotification)
            base.InsertItem(index, item);
        else
            Items.Insert(index, item);
    }

    protected override void RemoveItem(int index)
    {
        if (!_suppressNotification)
            base.RemoveItem(index);
        else
            Items.RemoveAt(index);
    }

    protected override void SetItem(int index, T item)
    {
        if (!_suppressNotification)
            base.SetItem(index, item);
        else
            Items[index] = item;
    }

    protected override void ClearItems()
    {
        if (!_suppressNotification)
            base.ClearItems();
        else
            Items.Clear();
    }

    public void AddRange(IEnumerable<T> items)
    {
        if (items == null) 
            throw new ArgumentNullException(nameof(items));

        _suppressNotification = true;

        foreach (var item in items) 
            Add(item);

        _suppressNotification = false;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public void AddTimeSorted(T item)
    {
        if (Items.Count == 0 || string.Compare(item.TimeStamp, Items[^1].TimeStamp, StringComparison.Ordinal) >= 0)
        {
            Add(item);
            return;
        }

        int index = Items.TakeWhile(existingItem =>
            string.Compare(existingItem.TimeStamp, item.TimeStamp, StringComparison.Ordinal) <= 0).Count();

        Insert(index, item);
    }

    public void AddRangeSorted(IEnumerable<T> items)
    {
        if (items == null) 
            throw new ArgumentNullException(nameof(items));

        _suppressNotification = true;

        foreach (var item in items) 
            AddTimeSorted(item);

        _suppressNotification = false;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
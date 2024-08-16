using LogAnalyzer.Models.Data.Containers;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace LogAnalyzer.Models.MainFeatures.LogAnalysis;

public class LogEntryCache : IList, IList<LogEntry>, INotifyCollectionChanged, INotifyPropertyChanged
{
    private readonly List<LogEntry> _items = [];

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _suppressNotifications;

    public int Count => _items.Count;
    public bool IsSynchronized => false;
    public object SyncRoot => ((ICollection)_items).SyncRoot;
    public bool IsReadOnly => false;
    public bool IsFixedSize => false;

    public void AddRangeTimeSorted(IEnumerable<LogEntry> items)
    {
        _suppressNotifications = true;
        
        var addedItems = new List<LogEntry>();
        foreach (LogEntry item in items)
        {
            AddTimeSorted(item);
            addedItems.Add(item);
        }
        _suppressNotifications = false;

        if (addedItems.Count > 0)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, addedItems, _items.Count - 1));
            OnPropertyChanged(nameof(Count));
        }
    }

    public void AddTimeSorted(LogEntry item)
    {
        if (_items.Count == 0 || string.Compare(item.TimeStamp, _items[^1].TimeStamp, StringComparison.Ordinal) >= 0)
        {
            Add(item);
            return;
        }

        int index = _items.TakeWhile(existingItem =>
            string.Compare(existingItem.TimeStamp, item.TimeStamp, StringComparison.Ordinal) <= 0).Count();

        Insert(index, item);
    }

    public void AddRange(IEnumerable<LogEntry> items)
    {
        _suppressNotifications = true;

        foreach (LogEntry item in items)
            Add(item);

        _suppressNotifications = false;
    }

    public void Add(LogEntry item)
    {
        _items.Add(item);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, _items.Count - 1));
        OnPropertyChanged(nameof(Count));
    }

    public int Add(object? value)
    {
        Add((LogEntry)value);
        return Count - 1;
    }

    public void Clear()
    {
        _items.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        OnPropertyChanged(nameof(Count));
    }

    public void Remove(object? value) => Remove((LogEntry)value);
    public bool Remove(LogEntry item)
    {
        int index = _items.IndexOf(item);
        if (index >= 0)
        {
            _items.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            OnPropertyChanged(nameof(Count));
            return true;
        }
        return false;
    }

    public void Insert(int index, object? value) => Insert(index, (LogEntry)value);
    public void Insert(int index, LogEntry item)
    {
        _items.Insert(index, item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        OnPropertyChanged(nameof(Count));
    }

    public void RemoveAt(int index)
    {
        LogEntry item = _items[index];
        _items.RemoveAt(index);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        OnPropertyChanged(nameof(Count));
    }

    public int IndexOf(object? value) => IndexOf((LogEntry)value);
    public int IndexOf(LogEntry item) => _items.IndexOf(item);
   
    public bool Contains(object? value) => Contains((LogEntry)value);
    public bool Contains(LogEntry item) => _items.Contains(item);
   
    public void CopyTo(Array array, int index) => CopyTo((LogEntry[])array, index);
    public void CopyTo(LogEntry[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);
  
    public IEnumerator<LogEntry> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

    object? IList.this[int index]
    {
        get => this[index];
        set => this[index] = (LogEntry)value;
    }
   
    public LogEntry this[int index]
    {
        get => _items[index];
        set
        {
            _items[index] = value;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, index));
        }
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if(!_suppressNotifications) 
            CollectionChanged?.Invoke(this, e);
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (!_suppressNotifications) 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
using LogAnalyzer.Core;
using LogAnalyzer.Models.Data.Containers;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.Models.MainFeatures.LogAnalysis;

public class LogAnalysisCache
{
    private const int BATCHSIZE = 100;
    private const int UPDATEINTERVALMS = 100;

    private readonly List<LogEntry> _logEntryBatch = [];
    private DateTime _lastUpdateTime = DateTime.MinValue;

    public event Action? OpenedFilesChanged;

    public ObservableCollection<FileInfo> OpenedFiles { get; } = [];
    public ObservableCollection<LogEntry> LogEntries { get; } = [];

    public BulkObservableCollection<LogEntry> LogEntryCache { get; } = new();

    public LogAnalysisCache()
    {
        OpenedFiles.CollectionChanged += OpenedFilesOnCollectionChanged;
    }

    public void AddLogEntryBatched(LogEntry logEntry)
    {
        _logEntryBatch.Add(logEntry);

        if (_logEntryBatch.Count >= BATCHSIZE || (DateTime.Now - _lastUpdateTime).TotalMilliseconds >= UPDATEINTERVALMS)
        {
            List<LogEntry> entriesToAdd = _logEntryBatch.ToList();
            _logEntryBatch.Clear();

            LogEntryCache.AddRangeSorted(entriesToAdd);

            _lastUpdateTime = DateTime.Now;
        }
    }

    public void Reset()
    {
        OpenedFiles.Clear();
        LogEntries.Clear();
        LogEntryCache.Clear();
    }

    private void OpenedFilesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OpenedFilesChanged?.Invoke();
    }
}
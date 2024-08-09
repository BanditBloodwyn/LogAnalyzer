using LogAnalyzer.Models.Data;
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
    public event Action? LogEntriesChanged;

    public ObservableCollection<FileInfo> OpenedFiles { get; } = [];
    public ObservableCollection<LogEntry> LogEntries { get; } = [];

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

            foreach (LogEntry entry in entriesToAdd)
                LogEntries.AddTimeSorted(entry);

            _lastUpdateTime = DateTime.Now;
           
            LogEntriesChanged?.Invoke();
        }
    }

    public void Reset()
    {
        OpenedFiles.Clear();
        LogEntries.Clear();
    }

    private void OpenedFilesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OpenedFilesChanged?.Invoke();
    }
}
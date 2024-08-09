using LogAnalyzer.Models.Data.Containers;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.Models.MainFeatures.LogAnalysis;

public class LogAnalysisCache
{
    public event Action? OpenedFilesChanged;
    public event Action? LogEntriesChanged;

    public ObservableCollection<FileInfo> OpenedFiles { get; } = [];
    public ObservableCollection<LogEntry> LogEntries { get; } = [];

    public LogAnalysisCache()
    {
        OpenedFiles.CollectionChanged += OpenedFilesOnCollectionChanged;
        LogEntries.CollectionChanged += LogEntriesOnCollectionChanged;
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

    private void LogEntriesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        LogEntriesChanged?.Invoke();
    }
}
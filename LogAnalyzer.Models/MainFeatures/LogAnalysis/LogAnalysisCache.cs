using LogAnalyzer.Models.Data.Containers;
using System.Collections.ObjectModel;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.Models.MainFeatures.LogAnalysis;

public class LogAnalysisCache
{
    public ObservableCollection<FileInfo> OpenedFiles { get; } = [];
    public ObservableCollection<LogEntry> LogEntries { get; } = [];

    public void Reset()
    {
        OpenedFiles.Clear();
        LogEntries.Clear();
    }
}
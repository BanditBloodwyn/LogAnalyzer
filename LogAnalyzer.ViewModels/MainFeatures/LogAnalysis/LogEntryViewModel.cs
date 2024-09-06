using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogEntryViewModel(LogEntry _logEntry) : ViewModelBase
{
    public long Index => _logEntry.LogIndex;
    public string TimeStamp => _logEntry.LogMessage?.TimeStamp.ToString("dd.MM.yyyy HH:mm:ss") ?? "";
    public string Source => _logEntry.LogMessage?.Sender ?? "";
    public string LogType => _logEntry.LogMessage?.Significance.ToString() ?? "";
    public string Message => _logEntry.LogMessage?.Message ?? "";
    public string InnerMessage => _logEntry.LogMessage?.Details ?? "";
    public int FileIndex => _logEntry.FileIndex;
    public RepositoryInteractionInformation? RepositoryInteractionInformation => _logEntry.RepositoryInteractionInformation;

    public bool HasInnerMessage => !string.IsNullOrEmpty(InnerMessage);
}
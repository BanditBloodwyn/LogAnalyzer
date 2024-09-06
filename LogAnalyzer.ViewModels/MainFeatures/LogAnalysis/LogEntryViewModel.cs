using Atbas.Core.Logging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogEntryViewModel(LogEntry _logEntry) : ViewModelBase
{
    public long LogIndex => _logEntry.LogIndex;
  
    public int FileIndex => _logEntry.FileIndex;
    public string TimeStamp => _logEntry.LogMessage?.TimeStamp.ToString("dd.MM.yyyy HH:mm:ss") ?? "";
    public string Source => _logEntry.LogMessage?.Sender ?? "";
    public MessageType? LogType => _logEntry.LogMessage?.Significance;
    public string Message => _logEntry.LogMessage?.Message ?? "";
    public string InnerMessage => _logEntry.LogMessage?.Details ?? "";
    
    public RepositoryInteractionInformation? RepositoryInteractionInformation => _logEntry.RepositoryInteractionInformation;

    public bool HasInnerMessage => !string.IsNullOrEmpty(InnerMessage);
}
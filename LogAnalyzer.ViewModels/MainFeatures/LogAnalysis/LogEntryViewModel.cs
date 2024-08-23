using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogEntryViewModel(LogEntry _logEntry) : ViewModelBase
{
    public long Index => _logEntry.index;
    public string TimeStamp => _logEntry.timeStamp;
    public string Source => _logEntry.source;
    public string LogType => _logEntry.logType;
    public string Message => _logEntry.message;
    public string InnerMessage => _logEntry.innerMessage;
    public int FileIndex => _logEntry.fileIndex;
    public RepositoryInteractionInformation? RepositoryInteractionInformation => _logEntry.repositoryInteractionInformation;

    public bool HasInnerMessage => !string.IsNullOrEmpty(InnerMessage);
}
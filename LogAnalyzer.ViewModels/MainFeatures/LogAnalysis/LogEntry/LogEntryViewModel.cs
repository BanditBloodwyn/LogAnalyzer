using Atbas.Core.Logging;
using Avalonia.Controls;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

public class LogEntryViewModel(
    Models.Data.Containers.LogEntry _logEntry,
    ContextMenuProvider _contextMenuProvider) 
    : ViewModelBase
{
    public event Action<long>? RequestShowCommunicationConnections;
    public event Action<long>? RequestRemoveCommunicationConnections;

    public long LogIndex => _logEntry.LogIndex;

    public int FileIndex => _logEntry.FileIndex;
    public string TimeStamp => _logEntry.LogMessage?.TimeStamp.ToString("dd.MM.yyyy HH:mm:ss") ?? "";
    public string Source => _logEntry.LogMessage?.Sender ?? "";
    public MessageType? LogType => _logEntry.LogMessage?.Significance;
    public string Message => _logEntry.LogMessage?.Message ?? "";
    public string InnerMessage => _logEntry.LogMessage?.Details ?? "";
    public RepositoryInteractionInformation? RepositoryInteractionInformation => _logEntry.RepositoryInteractionInformation;

    public bool HasInnerMessage => !string.IsNullOrEmpty(InnerMessage);

    private bool _connectionMarked;
    public bool ConnectionMarked
    {
        get => _connectionMarked;
        set => SetProperty(ref _connectionMarked, value);
    }

    public void OnPointerEntered()
    {
        long? communicationId = _logEntry.RepositoryInteractionInformation?.CommunicationID;

        if (communicationId.HasValue)
            RequestShowCommunicationConnections?.Invoke(communicationId.Value);
    }

    public void OnPointerExited()
    {
        long? communicationId = _logEntry.RepositoryInteractionInformation?.CommunicationID;

        if (communicationId.HasValue)
            RequestRemoveCommunicationConnections?.Invoke(communicationId.Value);
    }

    public MenuItem[] UpdateContextMenuContent(int clickedColumn) => _contextMenuProvider.ContextMenuContent(this, clickedColumn);

    public static int TimeComparison(LogEntryViewModel item1, LogEntryViewModel item2)
    {
        if (!DateTime.TryParse(item1.TimeStamp, out DateTime timeStamp1) ||
            !DateTime.TryParse(item2.TimeStamp, out DateTime timeStamp2))
            return 0;

        return timeStamp1.CompareTo(timeStamp2);
    }
}
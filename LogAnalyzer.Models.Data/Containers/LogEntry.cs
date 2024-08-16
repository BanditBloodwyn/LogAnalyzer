using LogAnalyzer.Core;

namespace LogAnalyzer.Models.Data.Containers;

public readonly struct LogEntry(string timeStamp, string source, string logType, string message, string innerMessage, int fileIndex) 
    : IHasTimestamp
{
    public string TimeStamp { get; } = timeStamp;
    public string Source { get; } = source;
    public string LogType { get; } = logType;
    public string Message { get; } = message;
    public string InnerMessage { get; } = innerMessage;
    public int FileIndex { get; } = fileIndex;

    public bool HasInnerMessage => !string.IsNullOrEmpty(innerMessage);
}
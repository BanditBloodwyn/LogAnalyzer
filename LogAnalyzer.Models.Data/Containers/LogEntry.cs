namespace LogAnalyzer.Models.Data.Containers;

public struct LogEntry(string timeStamp, string source, string logType, string message, string innerMessage)
{
    public string TimeStamp { get; } = timeStamp;
    public string Source { get; } = source;
    public string LogType { get; } = logType;
    public string Message { get; } = message;
    public string InnerMessage { get; } = innerMessage;
    public double? Offset { get; set; }
}
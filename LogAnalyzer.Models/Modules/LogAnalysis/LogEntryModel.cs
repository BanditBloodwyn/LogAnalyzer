namespace LogAnalyzer.Models.Modules.LogAnalysis;

public class LogEntryModel
{
    public DateTime TimeStamp { get; set; }
    public string Source { get; set; }
    public string LogType { get; set; }
    public string Message { get; set; }
    public string InnerMessage { get; set; }
}
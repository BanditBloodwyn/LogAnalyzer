using LogAnalyzer.Models.Data.Containers;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LogAnalyzer.Models.Strategies.LogStringParsing;

public class ClassicATBASLogStringParsing : ILogStringParsingStrategy
{
    public async Task<LogEntry> ParseLogString(
        string logString,
        CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(1, cancellationToken);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        return new LogEntry(
            GetTimetamp(logString), 
            GetSource(logString), 
            "", 
            logString, 
            "");
    }

    private static string GetTimetamp(string logString)
    {
        return DateTime.TryParse(ExtractTimestamp(logString), out DateTime timeStamp) 
            ? timeStamp.ToLongTimeString() 
            : string.Empty;
    }

    private static string ExtractTimestamp(string line)
    {
        string pattern = @"^\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2},\d{3}";
        Match match = Regex.Match(line, pattern);

        return match.Success 
            ? match.Value 
            : string.Empty;
    }

    private static string GetSource(string logString)
    {
        return logString
            .Split(':')[3]
            .Split(',')[0];
    }
}
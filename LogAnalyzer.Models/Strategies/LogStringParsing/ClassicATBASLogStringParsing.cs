using LogAnalyzer.Models.Data.Containers;
using System.Diagnostics;
using System.Globalization;
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
        string timestampString = ExtractTimestampString(logString);
        return DateTime.ParseExact(
                timestampString,
                "MM/dd/yyyy HH:mm:ss,fff",
                CultureInfo.InvariantCulture)
            .ToString(CultureInfo.CurrentCulture) + "," + timestampString.Split(',').Last();
    }

    private static string ExtractTimestampString(string line)
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
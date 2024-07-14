using LogAnalyzer.Models.Data.Containers;
using System.Diagnostics;

namespace LogAnalyzer.Models.Strategies.LogStringParsing;

public class ClassicATBASLogStringParsing : ILogStringParsingStrategy
{
    public async Task<LogEntry> ParseLogString(
        string logString,
        CancellationToken cancellationToken)
    {
        // REFACTOR: remove and add real functionality
        try
        {
            await Task.Delay(1, cancellationToken);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        return new LogEntry(DateTime.Now, "", "", logString, "");
    }
}
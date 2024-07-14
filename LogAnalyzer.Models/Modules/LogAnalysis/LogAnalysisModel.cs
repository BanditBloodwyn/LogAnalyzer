using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Strategies.LogStringFinding;
using LogAnalyzer.Models.Strategies.LogStringParsing;
using System.Diagnostics;
using FileInfo = System.IO.FileInfo;

namespace LogAnalyzer.Models.Modules.LogAnalysis;

public class LogAnalysisModel(
    ILogStringFindingStrategy _logFinder,
    ILogStringParsingStrategy _logParser)
    : ILogAnalysisModel
{
    public async Task AnalyzeAsync(
        string filePath,
        IProgress<LogEntry> logEntryProgress,
        IProgress<int> percentageProgress,
        CancellationToken cancellationToken)
    {
        try
        {
            long fileSize = new FileInfo(filePath).Length;
            long bytesRead = 0;

            await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            using StreamReader streamReader = new(fileStream);

            while(await _logFinder.FindLogEntries(streamReader, cancellationToken) is {} logEntryString)
            {
                LogEntry logEntry = await _logParser.ParseLogString(logEntryString, cancellationToken);
                logEntryProgress.Report(logEntry);

                bytesRead += logEntryString.Length + Environment.NewLine.Length;
                int percentComplete = (int)(bytesRead * 100 / fileSize);
                percentageProgress.Report(percentComplete);
            }

            percentageProgress.Report(100);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            GC.Collect();
        }
    }
}
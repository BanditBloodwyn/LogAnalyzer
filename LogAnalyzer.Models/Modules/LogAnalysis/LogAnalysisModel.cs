using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.Models.Modules.LogAnalysis;

public class LogAnalysisModel : ILogAnalysisModel
{
    public async Task AnalyzeAsync(
        string filePath,
        IProgress<LogEntry> logEntryProgress,
        IProgress<int> percentageProgress,
        CancellationToken cancellationToken)
    {
        long fileSize = new System.IO.FileInfo(filePath).Length;
        long bytesRead = 0;

        await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using StreamReader streamReader = new(fileStream);

        while (await streamReader.ReadLineAsync(cancellationToken) is { } line)
        {
            LogEntry logEntry = await ParseLogLine(line, cancellationToken);
            logEntryProgress.Report(logEntry);

            bytesRead += line.Length + Environment.NewLine.Length;
            int percentComplete = (int)((bytesRead * 100) / fileSize);
            percentageProgress.Report(percentComplete);
            
            if (cancellationToken.IsCancellationRequested) 
                GC.Collect();
        }

    }

    private static async Task<LogEntry> ParseLogLine(string line, CancellationToken cancellationToken)
    {
        // REFACTOR: remove and add real functionality
        await Task.Delay(1, cancellationToken);

        return new LogEntry(DateTime.Now, "", "", line, "");
    }
}
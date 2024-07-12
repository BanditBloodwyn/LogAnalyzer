
namespace LogAnalyzer.Models.Modules.LogAnalysis;

public class LogAnalysisModel : ILogAnalysisModel
{
    public async Task AnalyzeAsync(
        string filePath,
        IProgress<LogEntryModel> logEntryProgress,
        IProgress<int> percentageProgress)
    {
        long fileSize = new FileInfo(filePath).Length;
        long bytesRead = 0;

        await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using StreamReader streamReader = new(fileStream);

        while (await streamReader.ReadLineAsync() is { } line)
        {
            LogEntryModel? logEntry = await ParseLogLine(line);
            if (logEntry != null)
                logEntryProgress.Report(logEntry);

            bytesRead += line.Length + Environment.NewLine.Length;
            int percentComplete = (int)((bytesRead * 100) / fileSize);
            percentageProgress.Report(percentComplete);
        }
    }

    private static async Task<LogEntryModel?> ParseLogLine(string line)
    {
        // REFACTOR: remove and add real functionality
        await Task.Delay(1);

        return new LogEntryModel
        {
            TimeStamp = DateTime.Now,
            Message = line
        };
    }
}
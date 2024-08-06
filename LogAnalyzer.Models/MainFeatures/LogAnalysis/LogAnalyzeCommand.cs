using LogAnalyzer.Models.Data.BaseTypes.Commands;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Strategies.LogStringFinding;
using LogAnalyzer.Models.Strategies.LogStringParsing;
using System.Diagnostics;
using FileInfo = System.IO.FileInfo;

namespace LogAnalyzer.Models.MainFeatures.LogAnalysis;

public class LogAnalyzeCommand(
    ILogStringFindingStrategy _logFinder,
    ILogStringParsingStrategy _logParser)
    : ProgressCommand("Analyze log file")
{
    public Data.Containers.FileInfo? FileInfo { get; set; }
    public IProgress<LogEntry>? LogEntryProgress { get; set; }

    public override async Task Execute()
    {
        if (FileInfo?.Path == null || LogEntryProgress == null)
            return;

        MessageProgress?.Report(Path.GetFileName(FileInfo.Path));
        
        try
        {
            long fileSize = new FileInfo(FileInfo.Path).Length;
            long bytesRead = 0;

            await using FileStream fileStream = new(FileInfo.Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            using StreamReader streamReader = new(fileStream);

            while (await _logFinder.FindLogEntries(streamReader, CancellationToken) is { } logEntryString)
            {
                LogEntry logEntry = await _logParser.ParseLogString(logEntryString, CancellationToken, FileInfo.FileIndex);
                LogEntryProgress.Report(logEntry);

                bytesRead += logEntryString.Length + Environment.NewLine.Length;
                int percentComplete = (int)(bytesRead * 100 / fileSize);
                PercentsProgress?.Report(percentComplete);
            }

            PercentsProgress?.Report(100);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            GC.Collect();
        }
    }
}
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
    public LogAnalysisCache? Cache { get; set; }

    public override async Task Execute()
    {
        if (FileInfo?.Path == null || Cache == null)
            return;

        MessageProgress?.Report(Path.GetFileName(FileInfo.Path));
        
        try
        {
            long fileSize = new FileInfo(FileInfo.Path).Length;
            long bytesRead = 0;
            long logEntryIndex = 0;

            await using FileStream fileStream = new(FileInfo.Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            using StreamReader streamReader = new(fileStream);

            while (await _logFinder.FindLogEntries(streamReader, CancellationToken) is { } logEntryString)
            {
                logEntryIndex++;
                LogEntry logEntry = await _logParser.ParseLogString(logEntryIndex, logEntryString, CancellationToken, FileInfo.FileIndex);
                Cache.LogEntries.Add(logEntry);

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
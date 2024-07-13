using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.Models.Modules.LogAnalysis;

public interface ILogAnalysisModel
{
    public Task AnalyzeAsync(
        string filePath, 
        IProgress<LogEntry> logEntryProgress, 
        IProgress<int> percentageProgress, 
        CancellationToken cancellationToken);
}
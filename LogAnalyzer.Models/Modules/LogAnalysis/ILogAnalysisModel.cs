namespace LogAnalyzer.Models.Modules.LogAnalysis;

public interface ILogAnalysisModel
{
    public Task AnalyzeAsync(string filePath, IProgress<LogEntryModel> logEntryProgress, IProgress<int> percentageProgress);
}
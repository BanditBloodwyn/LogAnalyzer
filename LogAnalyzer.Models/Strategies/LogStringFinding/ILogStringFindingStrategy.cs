namespace LogAnalyzer.Models.Strategies.LogStringFinding;

public interface ILogStringFindingStrategy
{
    public Task<string?> FindLogEntries(
        StreamReader reader,
        CancellationToken cancellationToken);
}
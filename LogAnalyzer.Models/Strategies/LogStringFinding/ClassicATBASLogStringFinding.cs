namespace LogAnalyzer.Models.Strategies.LogStringFinding;

public class ClassicATBASLogStringFinding : ILogStringFindingStrategy
{
    public async Task<string?> FindLogEntries(
        StreamReader reader,
        CancellationToken cancellationToken)
    {
        if (await reader.ReadLineAsync(cancellationToken) is { } line)
            return line;
        return null;
    }
}
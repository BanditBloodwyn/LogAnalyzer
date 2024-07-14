using System.Text;
using System.Text.RegularExpressions;

namespace LogAnalyzer.Models.Strategies.LogStringFinding;

public class ClassicATBASLogStringFinding : ILogStringFindingStrategy
{
    private readonly StringBuilder _currentEntryBuilder = new();

    public async Task<string?> FindLogEntries(
        StreamReader reader,
        CancellationToken cancellationToken)
    {
        while (await reader.ReadLineAsync(cancellationToken) is { } line)
        {
            if(string.IsNullOrEmpty(line))
                continue;

            if (StartsWithTimestamp(line) && _currentEntryBuilder.Length > 0)
            {
                // We've found the start of a new entry, return the previous one
                string completeEntry = _currentEntryBuilder.ToString().TrimEnd();
                _currentEntryBuilder.Clear();
                _currentEntryBuilder.AppendLine(line);
                return completeEntry;
            }

            _currentEntryBuilder.AppendLine(line);
        }

        // Return the last entry if there's any content
        if (_currentEntryBuilder.Length > 0)
        {
            string lastEntry = _currentEntryBuilder.ToString().TrimEnd();
            _currentEntryBuilder.Clear();
            return lastEntry;
        }

        return null;
    }

    private static bool StartsWithTimestamp(string line)
    {
        string pattern = @"^\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2},\d{3}";
        return Regex.IsMatch(line, pattern);
    }
}
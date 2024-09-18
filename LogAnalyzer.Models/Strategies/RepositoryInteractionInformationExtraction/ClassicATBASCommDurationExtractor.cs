using System.Text.RegularExpressions;

namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public class ClassicATBASCommDurationExtractor : ICommDurationExtractor
{
    public double? ExtractMilliseconds(string text)
    {
        string pattern = @"\((\d+)ms\)";
        Match match = Regex.Match(text, pattern);

        if (!match.Success)
            return null;

        string millisecondsString = match.Groups[1].Value;

        return double.TryParse(millisecondsString, out double milliSeconds)
            ? milliSeconds
            : null;
    }
}
using System.Text.RegularExpressions;

namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public class ClassicATBASCommResultCountExtractor : ICommResultCountExtractor
{
    public double? ExtractResultCount(string text)
    {
        string pattern = @"\((\d+) results\)";
        Match match = Regex.Match(text, pattern);

        if (!match.Success)
            return null;

        string resultsCountString = match.Groups[1].Value;

        return double.TryParse(resultsCountString, out double resultsCount)
            ? resultsCount
            : null;
    }
}
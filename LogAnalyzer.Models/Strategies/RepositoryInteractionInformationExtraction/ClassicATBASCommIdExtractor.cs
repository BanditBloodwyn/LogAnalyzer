using System.Text.RegularExpressions;

namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public class ClassicATBASCommIdExtractor : ICommIdExtractor
{
    public long? ExtractCommId(string text)
    {
        long? commId = ExtractByRegex(text, @"\[(\d+)\]");
        if (commId != null)
            return commId;

        commId = ExtractByRegex(text, @"<(\d+)>");
        return commId;
    }

    private static long? ExtractByRegex(string text, string pattern)
    {
        Match match = Regex.Match(text, pattern);
        
        if (match.Success)
        {
            string communicationIdString = match.Groups[1].Value;
            return long.TryParse(communicationIdString, out long communicationId)
                ? communicationId
                : null;
        }

        return null;
    }
}
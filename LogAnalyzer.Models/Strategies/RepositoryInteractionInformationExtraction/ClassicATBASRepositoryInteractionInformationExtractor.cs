using LogAnalyzer.Models.Data.Containers;
using System.Text.RegularExpressions;

namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public class ClassicATBASRepositoryInteractionInformationExtractor : IRepositoryInteractionInformationExtractor
{
    private const double DURATION_WARNINGTHRESHOLD = 100;
    private const double DURATION_CRITICALTHRESHOLD = 1000;

    public RepositoryInteractionInformation? Extract(string message, string? innerMessage)
    {
        long? communicationId = ExtractCommId(message);
        double? milliSeconds = ExtractMilliseconds(message);

        if (!milliSeconds.HasValue && !communicationId.HasValue)
            return null;

        double resultsCount = ExtractResultCount(message, innerMessage);
        
        RepositoryInteractionInformation repoInfo = new RepositoryInteractionInformation
        {
            DurationMS = milliSeconds,
            ResultCount = resultsCount,
            CommunicationID = communicationId,
            HasDurationWarning = milliSeconds >= DURATION_WARNINGTHRESHOLD && milliSeconds < DURATION_CRITICALTHRESHOLD,
            HasCriticalDuration = milliSeconds >= DURATION_CRITICALTHRESHOLD,
        };

        return repoInfo;
    }

    private static long? ExtractCommId(string message)
    {
        string pattern = @"\[(\d+)\]";
        Match match = Regex.Match(message, pattern);

        if (!match.Success)
            return null;

        string communicationIdString = match.Groups[1].Value;
        return long.TryParse(communicationIdString, out long communicationId)
            ? communicationId
            : null;
    }

    private static double? ExtractMilliseconds(string message)
    {
        string pattern = @"\((\d+)ms\)";
        Match match = Regex.Match(message, pattern);

        if (!match.Success)
            return null;
        
        string millisecondsString = match.Groups[1].Value;

        return double.TryParse(millisecondsString, out double milliSeconds) 
            ? milliSeconds 
            : null;
    }

    private static double ExtractResultCount(string message, string? innerMessage)
    {
        string pattern = @"\((\d+) results\)";
        Match match = Regex.Match(message, pattern);

        if (!match.Success)
            return 0;

        string resultsCountString = match.Groups[1].Value;

        return double.TryParse(resultsCountString, out double resultsCount) 
            ? resultsCount
            : 0;
    }
}
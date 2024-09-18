using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public class ClassicATBASRepositoryInteractionInformationExtractor(
    ICommIdExtractor commIdExtractor,
    ICommDurationExtractor durationExtractor,
    ICommResultCountExtractor resultCountExtractor)
    : IRepositoryInteractionInformationExtractor
{
    private const double DURATION_WARNINGTHRESHOLD = 100;
    private const double DURATION_CRITICALTHRESHOLD = 1000;

    public RepositoryInteractionInformation? Extract(string message, string? innerMessage)
    {
        long? communicationId = commIdExtractor.ExtractCommId(message);
        double? milliSeconds = durationExtractor.ExtractMilliseconds(message);

        if (!milliSeconds.HasValue && !communicationId.HasValue)
            return null;

        double? resultsCount = resultCountExtractor.ExtractResultCount(message);

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
}
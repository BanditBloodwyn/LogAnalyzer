namespace LogAnalyzer.Models.Data.Containers;

public struct RepositoryInteractionInformation
{
    public double? DurationMS { get; init; }
    public double? ResultCount { get; init; }
    public long? CommunicationID { get; init; }

    public bool HasDurationWarning { get; init; }
    public bool HasCriticalDuration { get; init; }
}
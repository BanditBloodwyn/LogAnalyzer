namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public interface ICommDurationExtractor
{
    public double? ExtractMilliseconds(string text);
}
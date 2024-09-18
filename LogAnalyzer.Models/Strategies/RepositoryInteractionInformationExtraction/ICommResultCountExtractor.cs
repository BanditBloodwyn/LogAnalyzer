namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public interface ICommResultCountExtractor
{
    public double? ExtractResultCount(string text);
}
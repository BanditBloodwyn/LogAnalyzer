namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public interface ICommIdExtractor
{
    public long? ExtractCommId(string text);
}
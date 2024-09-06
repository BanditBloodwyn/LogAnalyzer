using LogAnalyzer.Models.Data.Containers;

namespace LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;

public interface IRepositoryInteractionInformationExtractor
{
    public RepositoryInteractionInformation? Extract(string message, string? innerMessage);
}
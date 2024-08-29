namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public record FilterData(
    string[] toShow, 
    string[] toHide,
    IReadOnlyDictionary<string, bool> logTypeFilters,
    IReadOnlyDictionary<string, bool> specialFilters);
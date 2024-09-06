using Atbas.Core.Logging;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public record FilterData(
    string[] toShow, 
    string[] toHide,
    IReadOnlyDictionary<MessageType, bool> logTypeFilters,
    IReadOnlyDictionary<string, bool> specialFilters);
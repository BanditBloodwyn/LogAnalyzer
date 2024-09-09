using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public record FilterData(
    bool filterIsEmpty,
    string[] toShow,
    string[] toHide,
    FilterCheckboxViewModel[] checkboxFilters);
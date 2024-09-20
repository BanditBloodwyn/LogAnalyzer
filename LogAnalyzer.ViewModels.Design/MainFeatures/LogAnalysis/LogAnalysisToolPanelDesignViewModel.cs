using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

namespace LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;

public class LogAnalysisToolPanelDesignViewModel()
    : LogAnalysisToolPanelViewModel(new SearchPanelViewModel(), new FilterPanelViewModel());
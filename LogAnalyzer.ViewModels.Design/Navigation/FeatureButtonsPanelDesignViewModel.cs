using LogAnalyzer.ViewModels.MainFeatures;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.ViewModels.Design.Navigation;

public class FeatureButtonsPanelDesignViewModel() : FeatureButtonsPanelViewModel(
    new LogAnalysisMainViewModel(null!, null!),
    new SettingsMainViewModel(null!));
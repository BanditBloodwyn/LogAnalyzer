using LogAnalyzer.ViewModels.Design.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.Design.MainFeatures.Settings;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.ViewModels.Design.Navigation;

public class FeatureButtonsPanelDesignViewModel() : FeatureButtonsPanelViewModel(
    new LogAnalysisMainDesignViewModel(),
    new SettingsMainDesignViewModel());
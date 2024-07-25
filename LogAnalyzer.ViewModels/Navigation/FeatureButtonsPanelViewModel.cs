using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LogAnalyzer.ViewModels.MainFeatures;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

namespace LogAnalyzer.ViewModels.Navigation;

public class FeatureButtonsPanelViewModel(
    LogAnalysisMainViewModel logAnalysisFeature,
    SettingsMainViewModel settingsFeature)
    : ViewModelBase
{
    public ObservableCollection<MainFeatureViewModelBase> Features { get; set; } =
        [logAnalysisFeature, settingsFeature];

    public ICommand OpenFeatureCommand => new OpenFeatureCommand();
}
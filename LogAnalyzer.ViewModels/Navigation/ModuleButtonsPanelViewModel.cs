using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LogAnalyzer.ViewModels.MainFeatures;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

namespace LogAnalyzer.ViewModels.Navigation;

public class ModuleButtonsPanelViewModel(
    LogAnalysisMainViewModel logAnalysisModule,
    SettingsMainViewModel settingsModule)
    : ViewModelBase
{
    public ObservableCollection<MainModuleViewModelBase> Modules { get; set; } =
        [logAnalysisModule, settingsModule];

    public ICommand OpenModuleCommand => new OpenModuleCommand();
}
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands;
using LogAnalyzer.ViewModels.MainComponents;
using LogAnalyzer.ViewModels.MainComponents.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Navigation;

public class ModuleButtonsPanelViewModel(
    LogAnalysisModuleViewModel logAnalysisModule,
    SettingsModuleViewModel settingsModule)
    : ViewModelBase
{
    public ObservableCollection<MainModuleViewModelBase> Modules { get; set; } =
        [logAnalysisModule, settingsModule];

    public ICommand OpenModuleCommand => new OpenModuleCommand();
}
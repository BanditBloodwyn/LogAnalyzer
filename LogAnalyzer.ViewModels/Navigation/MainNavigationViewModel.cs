using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.Navigation;

public class MainNavigationViewModel(
    FeatureButtonsPanelViewModel moduleButtonsPanelVM,
    CommandsPanelViewModel commandsPanelVM) : ViewModelBase
{
    public FeatureButtonsPanelViewModel ModuleButtonsPanelVM { get; set; } = moduleButtonsPanelVM;
    public CommandsPanelViewModel CommandsPanelVM { get; set; } = commandsPanelVM;
}
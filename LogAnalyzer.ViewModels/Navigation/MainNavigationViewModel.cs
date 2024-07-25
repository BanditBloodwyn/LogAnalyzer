using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.Navigation;

public class MainNavigationViewModel(
    FeatureButtonsPanelViewModel featureButtonsPanelVM,
    CommandsPanelViewModel commandsPanelVM) : ViewModelBase
{
    public FeatureButtonsPanelViewModel FeatureButtonsPanelVm { get; set; } = featureButtonsPanelVM;
    public CommandsPanelViewModel CommandsPanelVM { get; set; } = commandsPanelVM;
}
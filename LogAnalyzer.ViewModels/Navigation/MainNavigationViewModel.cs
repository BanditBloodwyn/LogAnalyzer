namespace LogAnalyzer.ViewModels.Navigation;

public class MainNavigationViewModel(
    ModuleButtonsPanelViewModel moduleButtonsPanelVM,
    CommandsPanelViewModel commandsPanelVM) : ViewModelBase
{
    public ModuleButtonsPanelViewModel ModuleButtonsPanelVM { get; set; } = moduleButtonsPanelVM;
    public CommandsPanelViewModel CommandsPanelVM { get; set; } = commandsPanelVM;
}
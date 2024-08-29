using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;

namespace LogAnalyzer.ViewModels.Navigation;

public class MainNavigationViewModel : ViewModelBase
{
    public FeatureButtonsPanelViewModel FeatureButtonsPanelVm { get; set; }
    public ViewModelBase? ToolPanelVM { get; set; }
    public CommandsPanelViewModel CommandsPanelVM { get; set; }

    public MainNavigationViewModel(
        FeatureButtonsPanelViewModel featureButtonsPanelVm,
        CommandsPanelViewModel commandsPanelVm)
    {
        FeatureButtonsPanelVm = featureButtonsPanelVm;
        CommandsPanelVM = commandsPanelVm;

        EventBinding<ChangeOpenedFeatureEvent> changeOpenedFeatureEventBinding = new(ChangeToolPanel);
        EventBus<ChangeOpenedFeatureEvent>.Register(changeOpenedFeatureEventBinding);
    }

    private void ChangeToolPanel(ChangeOpenedFeatureEvent @event)
    {
        if (@event.Feature is IToolPanelProvider toolPanelProvider)
            ToolPanelVM = toolPanelProvider.ToolPanel;
        else
            ToolPanelVM = null;

        OnPropertyChanged(nameof(ToolPanelVM));
    }
}
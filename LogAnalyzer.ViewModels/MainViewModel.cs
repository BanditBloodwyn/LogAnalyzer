using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.ViewModels;

public class MainViewModel : ViewModelBase
{
    private MainFeatureViewModelBase? _currentFeature;

    public MainFeatureViewModelBase? CurrentFeature
    {
        get => _currentFeature;
        set => SetProperty(ref _currentFeature, value);
    }

    public ViewModelBase? NavigationViewModel { get; init; }

    public MainViewModel(MainNavigationViewModel navigationVM)
    {
        EventBinding<ChangeOpenedFeatureEvent> changeOpenedFeatureEventBinding = new(ChangeOpenedFeature);
        EventBus<ChangeOpenedFeatureEvent>.Register(changeOpenedFeatureEventBinding);

        NavigationViewModel = navigationVM;
    }

    private void ChangeOpenedFeature(ChangeOpenedFeatureEvent @event)
    {
        if (@event.Feature is IReactToPreOpeningComponent preOpeningComponent)
            preOpeningComponent.OnPreShown();

        CurrentFeature = @event.Feature;

        if (@event.Feature is IReactToPostOpeningComponent postOpeningComponent)
            postOpeningComponent.OnShown();

        if (CurrentFeature is IReactToOpeningViewModel reactToOpeningVM)
            reactToOpeningVM.OnShown();
    }
}

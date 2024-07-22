using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;
using LogAnalyzer.ViewModels.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.ViewModels;

public class MainViewModel : ViewModelBase
{
    private MainModuleViewModelBase? _currentModule;

    public MainModuleViewModelBase? CurrentModule
    {
        get => _currentModule;
        set => SetProperty(ref _currentModule, value);
    }

    public ViewModelBase NavigationViewModel { get; set; }

    public MainViewModel(IServiceProvider serviceProvider)
    {
        NavigationViewModel = serviceProvider.GetRequiredService<MainNavigationViewModel>();

        EventBinding<ChangeOpenedModuleEvent> changeOpenedModuleEventBinding = new(ChangeOpenedModule);
        EventBus<ChangeOpenedModuleEvent>.Register(changeOpenedModuleEventBinding);
    }

    private void ChangeOpenedModule(ChangeOpenedModuleEvent @event)
    {
        if (@event.Module is IReactToPreOpeningComponent preOpeningModule)
            preOpeningModule.OnPreShown();

        CurrentModule = @event.Module;

        if (@event.Module is IReactToPostOpeningComponent postOpeningModule)
            postOpeningModule.OnShown();

        if (CurrentModule is IReactToOpeningViewModel reactToOpeningVM)
            reactToOpeningVM.OnShown();
    }
}

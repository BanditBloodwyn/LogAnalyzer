using Avalonia.Controls;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;

namespace LogAnalyzer.ViewModels;

public class MainViewModel : ViewModelBase
{
    private UserControl? _currentModuleView;

    public UserControl? CurrentModuleView
    {
        get => _currentModuleView;
        set
        {
            _currentModuleView = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        EventBinding<ChangeOpenedModuleEvent> changeOpenedModuleEventBinding = new(ChangeOpenedModule);
        EventBus<ChangeOpenedModuleEvent>.Register(changeOpenedModuleEventBinding);
    }

    private void ChangeOpenedModule(ChangeOpenedModuleEvent @event)
    {
        if (@event.Module is IReactToPreOpeningComponent preOpeningModule)
            preOpeningModule.OnPreShown();

        CurrentModuleView = @event.Module.GetView();

        if (@event.Module is IReactToPostOpeningComponent postOpeningModule)
            postOpeningModule.OnShown();

        if (CurrentModuleView.DataContext is IReactToOpeningViewModel reactToOpeningVM)
            reactToOpeningVM.OnShown();
    }
}

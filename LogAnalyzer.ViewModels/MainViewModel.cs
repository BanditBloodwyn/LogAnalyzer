using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.Modules;
using LogAnalyzer.Core.Modules.Interfaces;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;
using LogAnalyzer.Models.Framework;
using LogAnalyzer.ViewModels.Commands;

namespace LogAnalyzer.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly FrameworkModel _frameworkModel = new();

    private UserControl? _currentModuleView;

    public ObservableCollection<MainViewModule> MainViewModules => _frameworkModel.MainViewModules;

    public ICommand OpenModuleCommand => new OpenModuleCommand();

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
        if (@event.Module is IReactToPreOpeningModule preOpeningModule)
            preOpeningModule.OnPreShown();

        CurrentModuleView = @event.Module.GetView();

        if (@event.Module is IReactToPostOpeningModule postOpeningModule)
            postOpeningModule.OnShown();

        if (CurrentModuleView.DataContext is IReactToOpeningViewModel reactToOpeningVM)
            reactToOpeningVM.OnShown();
    }
}

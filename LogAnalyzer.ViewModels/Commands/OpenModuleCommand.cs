using System.Windows.Input;
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Models.Events;

namespace LogAnalyzer.ViewModels.Commands;

public class OpenModuleCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is MainViewComponent module)
            EventBus<ChangeOpenedModuleEvent>.Raise(new ChangeOpenedModuleEvent(module));
    }
}
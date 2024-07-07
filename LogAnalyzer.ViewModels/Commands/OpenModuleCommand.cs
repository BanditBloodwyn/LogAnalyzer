using System.Windows.Input;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.Modules;
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
        if (parameter is MainViewModule module)
            EventBus<ChangeOpenedModuleEvent>.Raise(new ChangeOpenedModuleEvent(module));
    }
}
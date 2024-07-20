using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;
using System.Windows.Input;

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
        if (parameter is MainModuleViewModelBase module)
            EventBus<ChangeOpenedModuleEvent>.Raise(new ChangeOpenedModuleEvent(module));
    }
}
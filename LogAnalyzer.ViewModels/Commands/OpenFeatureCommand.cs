using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Commands;

public class OpenFeatureCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is MainFeatureViewModelBase feature)
            EventBus<ChangeOpenedFeatureEvent>.Raise(new ChangeOpenedFeatureEvent(feature));
    }
}
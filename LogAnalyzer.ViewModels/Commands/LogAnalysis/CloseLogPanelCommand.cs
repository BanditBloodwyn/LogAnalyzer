using LogAnalyzer.ViewModels.Modules.LogAnalysis;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Commands.LogAnalysis;

public class CloseLogPanelCommand(
    LogPanelViewModel _viewModel) 
    : ICommand
{
    public event EventHandler? CanExecuteChanged;
   
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _viewModel.RequestClose();
    }
}
using System.Windows.Input;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

namespace LogAnalyzer.ViewModels.Commands.LogAnalysis;

public class OpenNewLogPanelCommand(
    LogAnalysisMainViewModel _viewModel)
    : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _viewModel.OpenNewLogs();
    }
}
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Commands.LogAnalysis;

public class StartFilterCommand(
    LogAnalysisToolPanelViewModel viewModel) 
    : ICommand
{
    public event EventHandler? CanExecuteChanged;
  
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        viewModel.StartFiltering();
    }
}
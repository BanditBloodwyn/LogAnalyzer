using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Commands.LogAnalysis;

public class CloseLogPanelCommand(
    SplittedLogPanelViewModel _viewModel)
    : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is FileInfo fileInfo)
            _viewModel.RequestClose(fileInfo);
    }
}
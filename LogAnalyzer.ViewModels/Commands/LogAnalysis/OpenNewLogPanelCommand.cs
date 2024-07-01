using LogAnalyzer.Models.Modules;
using LogAnalyzer.Services.IO.FileDialog;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Commands.LogAnalysis;

public class OpenNewLogPanelCommand(
    LogAnalysisModel _model, 
    IFileDialogService _fileDialogService) 
    : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        string[] logFilePath = await _fileDialogService.OpenFileDialogAsync();
        //_model.AddNewLogToObserve();
    }
}
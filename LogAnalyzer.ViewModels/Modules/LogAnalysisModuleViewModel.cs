using LogAnalyzer.Models.Modules;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Modules;

public class LogAnalysisModuleViewModel(
    LogAnalysisModel _model,
    IFileDialogService _fileDialogService)
    : ViewModelBase
{
    public ICommand OpenNewLogPanelCommand => new OpenNewLogPanelCommand(this);

    public async void OpenNewLogs()
    {
        string[] logFilePath = await _fileDialogService.OpenFileDialogAsync();
        //_model.Analyze
    }
}
using System.Collections.ObjectModel;
using Avalonia.Platform.Storage;
using LogAnalyzer.Models.Modules;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogAnalysisModuleViewModel(
    LogAnalysisModel _model,
    IFileDialogService _fileDialogService)
    : ViewModelBase
{
    public ObservableCollection<LogPanelViewModel> OpenedLogPanels { get; set; } = new();

    public ICommand OpenNewLogPanelCommand => new OpenNewLogPanelCommand(this);

    public async void OpenNewLogs()
    {
        IStorageFile[] filesToOpen = (await _fileDialogService.OpenFileDialogAsync()).ToArray();
        OpenNewLogPanels(filesToOpen);
        //_model.Analyze

    }

    private void OpenNewLogPanels(IStorageFile[] filesToOpen)
    {
        foreach (IStorageFile file in filesToOpen)
        {
            LogPanelViewModel logPanelViewModel = new(file);
            OpenedLogPanels.Add(logPanelViewModel);
        }
    }
}
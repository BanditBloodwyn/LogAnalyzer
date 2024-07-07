using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Modules;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
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
        FileInfoModel[] filesToOpen = (await _fileDialogService.OpenFileDialogAsync()).ToArray();
        OpenNewLogPanels(filesToOpen);
        //_model.Analyze

    }

    private void OpenNewLogPanels(FileInfoModel[] filesToOpen)
    {
        foreach (FileInfoModel file in filesToOpen)
        {
            LogPanelViewModel logPanelViewModel = new(file);
            OpenedLogPanels.Add(logPanelViewModel);
        }
    }
}
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogAnalysisModuleViewModel(
    IFileDialogService _fileDialogService)
    : ViewModelBase
{
    public ObservableCollection<LogPanelViewModel> OpenedLogPanels { get; } = new();

    public ICommand OpenNewLogPanelCommand => new OpenNewLogPanelCommand(this);

    public async void OpenNewLogs()
    {
        FileInfoModel[] filesToOpen = (await _fileDialogService.OpenFileDialogAsync()).ToArray();
        OpenNewLogPanels(filesToOpen);
    }

    private void OpenNewLogPanels(FileInfoModel[] filesToOpen)
    {
        foreach (FileInfoModel file in filesToOpen)
        {
            LogPanelViewModel logPanelViewModel = new(file);
            logPanelViewModel.RequestCloseEvent += panel => OpenedLogPanels.Remove(panel);

            OpenedLogPanels.Add(logPanelViewModel);

            logPanelViewModel.StartLogAnalysisAndDisplay();
        }
    }
}
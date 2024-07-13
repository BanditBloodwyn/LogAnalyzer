using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Modules.LogAnalysis;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogAnalysisModuleViewModel : ViewModelBase
{
    private readonly ILogAnalysisModel _logAnalysisModel1;
    private readonly IFileDialogService _fileDialogService1;

    public ObservableCollection<LogPanelViewModel> OpenedLogPanels { get; } = new();

    public ICommand OpenNewLogPanelCommand { get; init; }

    public LogAnalysisModuleViewModel(
        ILogAnalysisModel logAnalysisModel,
        IFileDialogService fileDialogService)
    {
        _logAnalysisModel1 = logAnalysisModel;
        _fileDialogService1 = fileDialogService;

        OpenNewLogPanelCommand = new OpenNewLogPanelCommand(this);
    }

    public async void OpenNewLogs()
    {
        Models.Data.Containers.FileInfo[] filesToOpen = (await _fileDialogService1.OpenFileDialogAsync()).ToArray();
        OpenNewLogPanels(filesToOpen);
    }

    private void OpenNewLogPanels(Models.Data.Containers.FileInfo[] filesToOpen)
    {
        foreach (Models.Data.Containers.FileInfo file in filesToOpen)
        {
            LogPanelViewModel logPanelViewModel = new(_logAnalysisModel1);
            logPanelViewModel.RequestCloseEvent += panel => OpenedLogPanels.Remove(panel);

            OpenedLogPanels.Add(logPanelViewModel);

            logPanelViewModel.StartLogAnalysisAndDisplay(file);
        }
    }
}
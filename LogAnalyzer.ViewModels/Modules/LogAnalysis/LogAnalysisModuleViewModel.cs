using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Modules.LogAnalysis;
using LogAnalyzer.Resources;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogAnalysisModuleViewModel : MainModuleViewModelBase
{
    private readonly ILogAnalysisModel _logAnalysisModel;
    private readonly IFileDialogService _fileDialogService;

    public override int NavigationIndex => 0;
    public override string ModuleHeader => "Log Analysis";
    public override Bitmap ModuleIcon => DefaultIcons.LogAnalysisIcon;

    public ObservableCollection<LogPanelViewModel> OpenedLogPanels { get; } = [];

    public ICommand OpenNewLogPanelCommand { get; init; }

    public LogAnalysisModuleViewModel(
        ILogAnalysisModel logAnalysisModel,
        IFileDialogService fileDialogService)
    {
        _logAnalysisModel = logAnalysisModel;
        _fileDialogService = fileDialogService;

        OpenNewLogPanelCommand = new OpenNewLogPanelCommand(this);
    }

    public async void OpenNewLogs()
    {
        Models.Data.Containers.FileInfo[] filesToOpen = (await _fileDialogService.OpenFileDialogAsync()).ToArray();
        OpenNewLogPanels(filesToOpen);
    }

    private void OpenNewLogPanels(Models.Data.Containers.FileInfo[] filesToOpen)
    {
        foreach (Models.Data.Containers.FileInfo file in filesToOpen)
        {
            LogPanelViewModel logPanelViewModel = new(_logAnalysisModel);
            logPanelViewModel.RequestCloseEvent += panel => OpenedLogPanels.Remove(panel);

            OpenedLogPanels.Add(logPanelViewModel);

            logPanelViewModel.StartLogAnalysisAndDisplay(file);
        }
    }
}
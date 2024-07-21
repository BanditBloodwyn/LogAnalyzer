using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogAnalysisModuleViewModel : MainModuleViewModelBase
{
    private readonly IFileDialogService _fileDialogService;
    private readonly ViewModelFactory.CreateLogPanel _logPanelFactory;

    public override int NavigationIndex => 0;
    public override string ModuleHeader => "Log Analysis";
    public override Bitmap ModuleIcon => DefaultIcons.LogAnalysisIcon;

    public ObservableCollection<LogPanelViewModel> OpenedLogPanels { get; } = [];

    public ICommand OpenNewLogPanelCommand { get; init; }

    public LogAnalysisModuleViewModel(
        IFileDialogService fileDialogService,
        ViewModelFactory.CreateLogPanel logPanelFactory)
    {
        _fileDialogService = fileDialogService;
        _logPanelFactory = logPanelFactory;

        OpenNewLogPanelCommand = new OpenNewLogPanelCommand(this);
    }

    public async void OpenNewLogs()
    {
        FileInfo[] filesToOpen = (await _fileDialogService.OpenFileDialogAsync()).ToArray();
        OpenNewLogPanels(filesToOpen);
    }

    private void OpenNewLogPanels(FileInfo[] filesToOpen)
    {
        foreach (FileInfo file in filesToOpen)
        {
            LogPanelViewModel logPanelViewModel = _logPanelFactory();
            logPanelViewModel.RequestCloseEvent += panel => OpenedLogPanels.Remove(panel);

            OpenedLogPanels.Add(logPanelViewModel);

            logPanelViewModel.StartLogAnalysisAndDisplay(file);
        }
    }
}
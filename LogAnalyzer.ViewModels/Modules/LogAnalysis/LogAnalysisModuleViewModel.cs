using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogAnalysisModuleViewModel(
    IFileDialogService fileDialogService,
    ViewModelFactory.CreateLogPanel logPanelFactory)
    : MainModuleViewModelBase
{
    public override int NavigationIndex => 0;
    public override string ModuleHeader => "Log Analysis";
    public override Bitmap ModuleIcon => DefaultIcons.LogAnalysisIcon;

    public ObservableCollection<LogPanelViewModel> OpenedLogPanels { get; } = [];

    private ICommand? _openNewLogPanelCommand;
    public ICommand OpenNewLogPanelCommand => _openNewLogPanelCommand ??= new OpenNewLogPanelCommand(this);

    public async void OpenNewLogs()
    {
        FileInfo[] filesToOpen = (await fileDialogService.OpenFileDialogAsync()).ToArray();
        OpenNewLogPanels(filesToOpen);
    }

    private void OpenNewLogPanels(FileInfo[] filesToOpen)
    {
        foreach (FileInfo file in filesToOpen)
        {
            LogPanelViewModel logPanelViewModel = logPanelFactory();
            logPanelViewModel.RequestCloseEvent += panel => OpenedLogPanels.Remove(panel);

            OpenedLogPanels.Add(logPanelViewModel);

            logPanelViewModel.StartLogAnalysisAndDisplay(file);
        }
    }
}
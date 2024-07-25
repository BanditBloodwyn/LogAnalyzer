using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogAnalysisMainViewModel(
    IFileDialogService fileDialogService,
    ViewModelFactory.CreateLogPanel logPanelFactory)
    : MainFeatureViewModelBase
{
    public override int NavigationIndex => 0;
    public override string FeatureHeader => "Log Analysis";
    public override Bitmap FeatureIcon => DefaultIcons.LogAnalysisIcon;

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
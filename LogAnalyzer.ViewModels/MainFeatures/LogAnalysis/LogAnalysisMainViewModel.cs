using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.MergedView;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogAnalysisMainViewModel(
    IFileDialogService _fileDialogService,
    SplittedLogPanelViewModel _splittedLogPanelVM,
    MergedLogPanelViewModel _mergedLogPanelVM)
    : MainFeatureViewModelBase
{
    public override int NavigationIndex => 0;
    public override string FeatureHeader => "Log Analysis";
    public override Bitmap FeatureIcon => DefaultIcons.LogAnalysisIcon;

    #region GUI Bindings

    private ILogPanel _logPanel = _mergedLogPanelVM;

    public ILogPanel LogPanel
    {
        get => _logPanel;
        set => SetProperty(ref _logPanel, value);
    }


    private ICommand? _openNewLogPanelCommand;
    public ICommand OpenNewLogPanelCommand => _openNewLogPanelCommand ??= new OpenNewLogPanelCommand(this);

    private ICommand? _switchViewCommand;
    public ICommand SwitchViewCommand => _switchViewCommand ??= new SwitchViewCommand(this);

    #endregion

    public async void OpenNewLogs()
    {
        FileInfo[] filesToOpen = (await _fileDialogService.OpenFileDialogAsync()).ToArray();
        LogPanel.OpenFiles(filesToOpen);
    }

    public void SwitchView()
    {
        LogPanel = LogPanel switch
        {
            SplittedLogPanelViewModel => _mergedLogPanelVM,
            MergedLogPanelViewModel => _splittedLogPanelVM,
            _ => LogPanel
        };
    }
}
using Avalonia.Media.Imaging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.MergedView;
using System.Windows.Input;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;
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

    public MergedLogPanelViewModel MergedLogPanel { get; set; } = _mergedLogPanelVM;


    private ICommand? _openNewLogPanelCommand;
    public ICommand OpenNewLogPanelCommand => _openNewLogPanelCommand ??= new OpenNewLogPanelCommand(this);

    #endregion

    public async void OpenNewLogs()
    {
        FileInfo[] filesToOpen = (await _fileDialogService.OpenFileDialogAsync()).ToArray();
        MergedLogPanel.OpenFiles(filesToOpen);
    }
}
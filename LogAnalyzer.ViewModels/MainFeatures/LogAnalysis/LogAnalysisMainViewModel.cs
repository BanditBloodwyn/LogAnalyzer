using Avalonia.Media.Imaging;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Resources;
using LogAnalyzer.Services.IO.FileDialog;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.MergedView;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogAnalysisMainViewModel : MainFeatureViewModelBase, IToolPanelProvider
{
    private readonly IFileDialogService _fileDialogService1;
    private readonly SplittedLogPanelViewModel _splittedLogPanelVm;
    private readonly MergedLogPanelViewModel _mergedLogPanelVm;
    private readonly LogAnalysisToolPanelViewModel _toolPanelVm;

    public override int NavigationIndex => 0;
    public override string FeatureHeader => "Log Analysis";
    public override Bitmap FeatureIcon => DefaultIcons.LogAnalysisIconWhite;

    #region GUI Bindings
   
    public ViewModelBase ToolPanel => _toolPanelVm;

    private LogPanelBaseViewModel _logPanel;
    public LogPanelBaseViewModel LogPanel
    {
        get => _logPanel;
        set => SetProperty(ref _logPanel, value);
    }

    private ICommand? _openNewLogPanelCommand;
    public ICommand OpenNewLogPanelCommand => _openNewLogPanelCommand ??= new OpenNewLogPanelCommand(this);

    private ICommand? _switchViewCommand;
    public ICommand SwitchViewCommand => _switchViewCommand ??= new SwitchViewCommand(this);

    #endregion

    public LogAnalysisMainViewModel(IFileDialogService fileDialogService,
        SplittedLogPanelViewModel splittedLogPanelVm,
        MergedLogPanelViewModel mergedLogPanelVm,
        LogAnalysisToolPanelViewModel toolPanelVm)
    {
        _fileDialogService1 = fileDialogService;
        _splittedLogPanelVm = splittedLogPanelVm;
        _mergedLogPanelVm = mergedLogPanelVm;
        _toolPanelVm = toolPanelVm;
        _logPanel = mergedLogPanelVm;

        _toolPanelVm.StartFilteringRequested += OnStartFilteringRequested;
    }

    private void OnStartFilteringRequested(FilterData filter)
    {
        _logPanel.SetFilter(filter);
    }

    public async void OpenNewLogs()
    {
        FileInfo[] filesToOpen = (await _fileDialogService1.OpenFileDialogAsync()).ToArray();
        LogPanel.OpenFiles(filesToOpen);
    }

    public void SwitchView()
    {
        LogPanel = LogPanel switch
        {
            SplittedLogPanelViewModel => _mergedLogPanelVm,
            MergedLogPanelViewModel => _splittedLogPanelVm,
            _ => LogPanel
        };
    }
}
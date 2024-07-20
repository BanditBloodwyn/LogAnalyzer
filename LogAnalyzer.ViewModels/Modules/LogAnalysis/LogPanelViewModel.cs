using Avalonia.Threading;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.CommandQueue.Commands;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Events;
using LogAnalyzer.Models.Modules.LogAnalysis;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogPanelViewModel : ViewModelBase
{
    private readonly ILogAnalysisModel _logAnalysisModel;

    private CancellationTokenSource? _fileAnalysisCtSource;


    #region GUI Bindings

    public ObservableCollection<LogEntry> LogEntries { get; } = [];

    private FileInfo _file;
    public FileInfo File
    {
        get => _file;
        protected set => SetProperty(ref _file, value);
    }

    private bool _isAnalyzing;
    public bool IsAnalyzing
    {
        get => _isAnalyzing;
        protected set => SetProperty(ref _isAnalyzing, value);
    }

    private int _analysisProgressPercents;
    public int AnalysisProgressPercents
    {
        get => _analysisProgressPercents;
        protected set => SetProperty(ref _analysisProgressPercents, value);
    }

    public ICommand CloseLogPanelCommand { get; init; }

    #endregion


    public event Action<LogPanelViewModel>? RequestCloseEvent;


    #region Ctor

    public LogPanelViewModel(ILogAnalysisModel logAnalysisModel)
    {
        _logAnalysisModel = logAnalysisModel;

        CloseLogPanelCommand = new CloseLogPanelCommand(this);
    }

    protected LogPanelViewModel() { }

    #endregion


    public void RequestClose()
    {
        RequestCloseEvent?.Invoke(this);
        Reset();
    }

    public void StartLogAnalysisAndDisplay(FileInfo file)
    {
        File = file;
        IsAnalyzing = true;
        AnalysisProgressPercents = 0;

        IProgress<LogEntry> entryProgress = new Progress<LogEntry>(OnLogEntryProcessed);
        IProgress<int> percentProgress = new Progress<int>(OnAnalysisProgressUpdated);

        _fileAnalysisCtSource = new();
        CancellationToken ct = _fileAnalysisCtSource.Token;

        EventBus<AddNewQueuedCommandEvent>.Raise(new AddNewQueuedCommandEvent(
            new InvokerCommand(
                "Analyze Log",
                () => _logAnalysisModel.AnalyzeAsync(File.Path, entryProgress, percentProgress, ct))));
    }

    private void OnLogEntryProcessed(LogEntry logEntry)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            LogEntries.Add(logEntry);
            OnPropertyChanged(nameof(LogEntries));
        });
    }

    private void OnAnalysisProgressUpdated(int progressPercentage)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            AnalysisProgressPercents = progressPercentage;
        });
    }

    private void Reset()
    {
        LogEntries.Clear();
        IsAnalyzing = false;
        AnalysisProgressPercents = 0;

        if (_fileAnalysisCtSource != null)
        {
            _fileAnalysisCtSource.Cancel();
            _fileAnalysisCtSource.Dispose();
            _fileAnalysisCtSource = null;
        }
    }
}
using Avalonia.Threading;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Events;
using LogAnalyzer.ViewModels.Commands;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogPanelViewModel(
    CommandFactory.CreateLogAnalyzeCommand _commandFactory)
    : ViewModelBase
{
    // used for design víew model
    public LogPanelViewModel() : this(null!) { }

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

    private ICommand? _closeLogPanelCommand;
    public ICommand CloseLogPanelCommand => _closeLogPanelCommand ??= new CloseLogPanelCommand(this);

    #endregion


    public event Action<LogPanelViewModel>? RequestCloseEvent;

    public void RequestClose()
    {
        RequestCloseEvent?.Invoke(this);
        Reset();
    }

    public void StartLogAnalysisAndDisplay(FileInfo file)
    {
        File = file;
        IsAnalyzing = true;

        _fileAnalysisCtSource = new();
        CancellationToken ct = _fileAnalysisCtSource.Token;

        IProgress<LogEntry> entryProgress = new Progress<LogEntry>(OnLogEntryProcessed);

        LogAnalyzeCommand logAnalyzeCommand = _commandFactory();
        logAnalyzeCommand.CancellationToken = ct;
        logAnalyzeCommand.FilePath = file.FullName;
        logAnalyzeCommand.LogEntryProgress = entryProgress;

        EventBus<AddNewProgressCommandEvent>.Raise(
            new AddNewProgressCommandEvent(logAnalyzeCommand));
    }

    private void OnLogEntryProcessed(LogEntry logEntry)
    {
        Dispatcher.UIThread.Invoke(() => LogEntries.Add(logEntry));
    }

    private void Reset()
    {
        LogEntries.Clear();
        IsAnalyzing = false;

        if (_fileAnalysisCtSource != null)
        {
            _fileAnalysisCtSource.Cancel();
            _fileAnalysisCtSource.Dispose();
            _fileAnalysisCtSource = null;
        }
    }
}
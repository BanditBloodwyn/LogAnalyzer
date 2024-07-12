using Avalonia.Threading;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Modules.LogAnalysis;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.Modules.LogAnalysis;

public class LogPanelViewModel : ViewModelBase
{
    private FileInfoModel _file;
    private bool _isAnalyzing;
    private int _analysisProgressPercents;
    private readonly ILogAnalysisModel _logAnalysisModel1;

    #region GUI Bindings

    public FileInfoModel File
    {
        get => _file;
        protected set => SetProperty(ref _file, value);
    }
    public ObservableCollection<LogEntryModel> LogEntries { get; } = [];
    public bool IsAnalyzing
    {
        get => _isAnalyzing;
        protected set => SetProperty(ref _isAnalyzing, value);
    }
    public int AnalysisProgressPercents
    {
        get => _analysisProgressPercents;
        protected set => SetProperty(ref _analysisProgressPercents, value);
    }

    public ICommand CloseLogPanelCommand { get; init; }
    
    #endregion


    public event Action<LogPanelViewModel>? RequestCloseEvent;

    
    #region Ctor

    public LogPanelViewModel(ILogAnalysisModel _logAnalysisModel)
    {
        _logAnalysisModel1 = _logAnalysisModel;

        CloseLogPanelCommand = new CloseLogPanelCommand(this);
    }

    public LogPanelViewModel() { }

    #endregion

    public void RequestClose()
    {
        RequestCloseEvent?.Invoke(this);
    }

    public async void StartLogAnalysisAndDisplay(FileInfoModel file)
    {
        File = file;
        IsAnalyzing = true;
        AnalysisProgressPercents = 0;

        IProgress<LogEntryModel> entryProgress = new Progress<LogEntryModel>(OnLogEntryProcessed);
        IProgress<int> percentProgress = new Progress<int>(OnAnalysisProgressUpdated);

        try
        {
            await _logAnalysisModel1.AnalyzeAsync(File.Path, entryProgress, percentProgress);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsAnalyzing = false;
            AnalysisProgressPercents = 100;
        }
    }

    private void OnLogEntryProcessed(LogEntryModel logEntry)
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
}
using Avalonia.Threading;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Events;
using LogAnalyzer.Models.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.Commands;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogPanelBaseViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory)
    : ViewModelBase
{
    private const int BATCHSIZE = 100;
    private const int UPDATEINTERVALMS = 100;
    
    private readonly List<LogEntry> _logEntryBatch = [];
    private DateTime _lastUpdateTime = DateTime.MinValue;

    public LogAnalysisCache Cache { get; } = new();

    public void OpenFiles(FileInfo[] filesToOpen)
    {
        Cache.Reset();

        foreach (FileInfo fileInfo in filesToOpen)
            Cache.OpenedFiles.Add(fileInfo);

        foreach (FileInfo fileInfo in filesToOpen)
            CreateLogAnalyzeCommand(fileInfo);
    }

    private void CreateLogAnalyzeCommand(FileInfo fileInfo)
    {
        IProgress<LogEntry> entryProgress = new Progress<LogEntry>(
            OnLogEntryProcessed);

        LogAnalyzeCommand logAnalyzeCommand = _commandFactory();
        logAnalyzeCommand.FileInfo = fileInfo;
        logAnalyzeCommand.LogEntryProgress = entryProgress;

        EventBus<AddNewProgressCommandEvent>.Raise(
            new AddNewProgressCommandEvent(logAnalyzeCommand));
    }

    private void OnLogEntryProcessed(LogEntry logEntry)
    {
        _logEntryBatch.Add(logEntry);

        if (_logEntryBatch.Count >= BATCHSIZE || (DateTime.Now - _lastUpdateTime).TotalMilliseconds >= UPDATEINTERVALMS)
            UpdateUI();
    }

    private void UpdateUI()
    {
        List<LogEntry> entriesToAdd = _logEntryBatch.ToList();
        _logEntryBatch.Clear();

        Dispatcher.UIThread.InvokeAsync(() =>
        {
            foreach (LogEntry entry in entriesToAdd) 
                Cache.LogEntries.AddTimeSorted(entry);
        });

        _lastUpdateTime = DateTime.Now;
    }
}
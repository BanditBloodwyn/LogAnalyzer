using Avalonia.Threading;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Models.Events;
using LogAnalyzer.Models.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.MergedView;

public class MergedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory)
    : ViewModelBase, ILogPanel
{
    private LogAnalysisCache _cache = new();

    public ObservableCollection<FileInfo> OpenedFiles => _cache.OpenedFiles;
    public ObservableCollection<LogEntry> LogEntries => _cache.LogEntries;

    public void OpenFiles(FileInfo[] filesToOpen)
    {
        _cache.Reset();

        for (int index = 0; index < filesToOpen.Length; index++)
        {
            FileInfo fileInfo = filesToOpen[index];
            fileInfo.Assignment = new FileAssignment(index, filesToOpen.Length);

            _cache.OpenedFiles.Add(fileInfo);

            CreateLogAnalyzeCommand(fileInfo);
        }
    }

    private void CreateLogAnalyzeCommand(FileInfo fileInfo)
    {
        IProgress<LogEntry> entryProgress = new Progress<LogEntry>(
            logEntry => OnLogEntryProcessed(fileInfo.Assignment, logEntry));

        LogAnalyzeCommand logAnalyzeCommand = _commandFactory();
        logAnalyzeCommand.FileInfo = fileInfo;
        logAnalyzeCommand.LogEntryProgress = entryProgress;

        EventBus<AddNewProgressCommandEvent>.Raise(
            new AddNewProgressCommandEvent(logAnalyzeCommand));
    }

    private void OnLogEntryProcessed(FileAssignment? fileAssignment, LogEntry logEntry)
    {
        if (fileAssignment.HasValue)
            logEntry.FileAssignment = fileAssignment.Value;

        Dispatcher.UIThread.Invoke(() => _cache.LogEntries.AddTimeSorted(logEntry));
    }
}
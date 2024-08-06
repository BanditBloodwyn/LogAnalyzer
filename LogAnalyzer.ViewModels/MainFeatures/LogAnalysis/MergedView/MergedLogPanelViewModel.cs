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
    public ObservableCollection<FileInfo> OpenedFiles { get; } = [];
    public ObservableCollection<LogEntry> LogEntries { get; } = [];

    public void OpenFiles(FileInfo[] filesToOpen)
    {
        OpenedFiles.Clear();
        LogEntries.Clear();

        for (int index = 0; index < filesToOpen.Length; index++)
        {
            FileInfo fileInfo = filesToOpen[index];
            fileInfo.Assignment = new FileAssignment(index, filesToOpen.Length);

            OpenedFiles.Add(fileInfo);

            CreateLogAnalyzeCommand(fileInfo);
        }
    }

    private void CreateLogAnalyzeCommand(FileInfo fileInfo)
    {
        IProgress<LogEntry> entryProgress = new Progress<LogEntry>(
            logEntry => OnLogEntryProcessed(fileInfo.Assignment, logEntry));

        LogAnalyzeCommand logAnalyzeCommand = _commandFactory();
        logAnalyzeCommand.FilePath = fileInfo.FullName;
        logAnalyzeCommand.LogEntryProgress = entryProgress;

        EventBus<AddNewProgressCommandEvent>.Raise(
            new AddNewProgressCommandEvent(logAnalyzeCommand));
    }

    private void OnLogEntryProcessed(FileAssignment? fileAssignment, LogEntry logEntry)
    {
        if (fileAssignment.HasValue)
            logEntry.FileAssignment = fileAssignment.Value;

        Dispatcher.UIThread.Invoke(() => LogEntries.AddTimeSorted(logEntry));
    }
}
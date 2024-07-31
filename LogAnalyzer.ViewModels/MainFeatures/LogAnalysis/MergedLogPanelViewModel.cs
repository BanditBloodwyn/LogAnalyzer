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

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class MergedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory)
    : ViewModelBase
{
    public ObservableCollection<FileInfo> OpenedFiles { get; } = [];
    public ObservableCollection<LogEntry> LogEntries { get; } = [];

    private readonly List<double> _logEntryOffsets = [];

    public void OpenFiles(FileInfo[] filesToOpen)
    {
        OpenedFiles.Clear();
        LogEntries.Clear();
        
        _logEntryOffsets.Clear();
        _logEntryOffsets.AddRange(DistributePositions(-50, 50, filesToOpen.Length));

        for (int index = 0; index < filesToOpen.Length; index++)
        {
            FileInfo fileInfo = filesToOpen[index];
            fileInfo.Index = index;

            OpenedFiles.Add(fileInfo);
            CreateLogAnalyzeCommand(fileInfo);
        }
    }

    private void CreateLogAnalyzeCommand(FileInfo fileInfo)
    {
        IProgress<LogEntry> entryProgress = new Progress<LogEntry>(
            logEntry => OnLogEntryProcessed(fileInfo.Index, logEntry));

        LogAnalyzeCommand logAnalyzeCommand = _commandFactory();
        logAnalyzeCommand.FilePath = fileInfo.FullName;
        logAnalyzeCommand.LogEntryProgress = entryProgress;

        EventBus<AddNewProgressCommandEvent>.Raise(
            new AddNewProgressCommandEvent(logAnalyzeCommand));
    }

    private void OnLogEntryProcessed(int? index, LogEntry logEntry)
    {
        if(index.HasValue) 
            logEntry.Offset = _logEntryOffsets[index.Value];

        Dispatcher.UIThread.Invoke(() => LogEntries.AddTimeSorted(logEntry));
    }

    private static IEnumerable<double> DistributePositions(double min, double max, int count)
    {
        if (count < 2)
        {
            yield return 0;
            yield break;
        }
        
        for (int i = 0; i < count; i++)
            yield return min + (max - min) * i / (count - 1);
    }
}
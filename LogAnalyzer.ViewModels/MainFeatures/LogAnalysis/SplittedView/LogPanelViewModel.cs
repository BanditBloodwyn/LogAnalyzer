using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.Containers;
using System.Collections.ObjectModel;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;

public class LogPanelViewModel(FileInfo fileInfo) : ViewModelBase
{
    public FileInfo FileInfo { get; } = fileInfo;
    public ObservableCollection<LogEntry> LogEntries { get; } = [];
}
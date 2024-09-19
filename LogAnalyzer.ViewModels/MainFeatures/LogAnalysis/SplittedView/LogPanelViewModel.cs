using LogAnalyzer.Core.ViewsModels;
using System.Collections.ObjectModel;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;

public class LogPanelViewModel(FileInfo fileInfo) : ViewModelBase
{
    public FileInfo FileInfo { get; } = fileInfo;
    public ObservableCollection<Models.Data.Containers.LogEntry> LogEntries { get; } = [];
}
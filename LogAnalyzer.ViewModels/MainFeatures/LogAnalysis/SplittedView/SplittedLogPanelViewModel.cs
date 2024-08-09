using LogAnalyzer.Models.Data;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.Commands;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;

public class SplittedLogPanelViewModel : LogPanelBaseViewModel
{
    public ObservableCollection<LogPanelViewModel> FilePanels { get; } = [];

    private ICommand? _closeLogPanelCommand;
    public ICommand OpenNewLogPanelCommand => _closeLogPanelCommand ??= new CloseLogPanelCommand(this);

    public SplittedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory)
        : base(_commandFactory)
    {
        Cache.OpenedFilesChanged += OnOpenedFilesChanged;
        Cache.LogEntriesChanged += OnLogEntriesChanged;
    }

    public void RequestClose(FileInfo fileInfo)
    {
        LogPanelViewModel? viewModel = FilePanels.FirstOrDefault(file => file.FileInfo == fileInfo);
        if (viewModel != null)
            FilePanels.Remove(viewModel);
    }

    private void OnOpenedFilesChanged()
    {
        FilePanels.Clear();
        foreach (FileInfo file in Cache.OpenedFiles)
            FilePanels.Add(new LogPanelViewModel(file));
    }

    private void OnLogEntriesChanged()
    {
        foreach (LogEntry logEntry in Cache.LogEntries)
        {
            LogPanelViewModel? filePanel = FilePanels.FirstOrDefault(fp => fp.FileInfo.FileIndex == logEntry.FileIndex);

            if (filePanel != null && !filePanel.LogEntries.Contains(logEntry))
                filePanel.LogEntries.AddTimeSorted(logEntry);
        }
    }
}
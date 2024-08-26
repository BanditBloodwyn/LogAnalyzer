using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Events;
using LogAnalyzer.Models.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.Commands;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogPanelBaseViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory)
    : ViewModelBase
{
    public LogAnalysisCache Cache { get; } = new();

    public void OpenFiles(FileInfo[] filesToOpen)
    {
        Cache.Reset();
        Reset();

        foreach (FileInfo fileInfo in filesToOpen)
            Cache.OpenedFiles.Add(fileInfo);

        foreach (FileInfo fileInfo in filesToOpen)
            CreateLogAnalyzeCommand(fileInfo);
    }

    protected virtual void Reset()
    { }

    private void CreateLogAnalyzeCommand(FileInfo fileInfo)
    {
        LogAnalyzeCommand logAnalyzeCommand = _commandFactory();
        logAnalyzeCommand.FileInfo = fileInfo;
        logAnalyzeCommand.Cache = Cache;

        EventBus<AddNewProgressCommandEvent>.Raise(
            new AddNewProgressCommandEvent(logAnalyzeCommand));
    }
}
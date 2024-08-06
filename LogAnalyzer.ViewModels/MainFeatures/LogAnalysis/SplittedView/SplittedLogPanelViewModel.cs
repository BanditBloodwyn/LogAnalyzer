using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands;
using System.Collections.ObjectModel;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;

public class SplittedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand _commandFactory) 
    : ViewModelBase, ILogPanel
{
    public ObservableCollection<LogPanelViewModel> LogPanels { get; } = [];

    public void OpenFiles(FileInfo[] filesToOpen)
    {
        LogPanels.Clear();

        foreach (FileInfo file in filesToOpen)
        {
            LogPanelViewModel viewModel = new LogPanelViewModel(_commandFactory);
            LogPanels.Add(viewModel);
            viewModel.StartLogAnalysisAndDisplay(file);
        }
    }
}
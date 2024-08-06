using LogAnalyzer.Core.ViewsModels;
using System.Collections.ObjectModel;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;

public class SplittedLogPanelViewModel : ViewModelBase, ILogPanel
{
    public ObservableCollection<LogPanelViewModel> LogPanels { get; } = [];

    public void OpenFiles(FileInfo[] filesToOpen)
    {
        LogPanels.Clear();

        foreach (FileInfo file in filesToOpen)
        {
            LogPanelViewModel viewModel = new LogPanelViewModel();
            LogPanels.Add(viewModel);
            viewModel.StartLogAnalysisAndDisplay(file);
        }
    }
}
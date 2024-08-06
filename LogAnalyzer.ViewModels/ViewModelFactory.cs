using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;

namespace LogAnalyzer.ViewModels;

public class ViewModelFactory
{
    public delegate LogPanelViewModel CreateLogPanel();
}
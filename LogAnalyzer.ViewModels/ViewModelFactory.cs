using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

namespace LogAnalyzer.ViewModels;

public class ViewModelFactory
{
    public delegate LogPanelViewModel CreateLogPanel();
}
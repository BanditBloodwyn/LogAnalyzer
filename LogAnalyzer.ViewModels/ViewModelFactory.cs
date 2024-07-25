using LogAnalyzer.ViewModels.MainComponents.LogAnalysis;

namespace LogAnalyzer.ViewModels;

public class ViewModelFactory
{
    public delegate LogPanelViewModel CreateLogPanel();
}
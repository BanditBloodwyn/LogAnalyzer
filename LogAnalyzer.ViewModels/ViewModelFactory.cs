using LogAnalyzer.ViewModels.Modules.LogAnalysis;

namespace LogAnalyzer.ViewModels;

public class ViewModelFactory
{
    public delegate LogPanelViewModel CreateLogPanel();
}
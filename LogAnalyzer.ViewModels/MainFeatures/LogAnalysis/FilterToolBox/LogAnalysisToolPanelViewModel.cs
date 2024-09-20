using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class LogAnalysisToolPanelViewModel : ViewModelBase
{
    public event Action<FilterData>? StartFilteringRequested;

    public SearchPanelViewModel SearchPanelVM { get; init; }
    public FilterPanelViewModel FilterPanelVM { get; init; }

    public LogAnalysisToolPanelViewModel(
        SearchPanelViewModel searchPanelVm,
        FilterPanelViewModel filterPanelVm)
    {
        SearchPanelVM = searchPanelVm;
        FilterPanelVM = filterPanelVm;

        FilterPanelVM.StartFilteringRequested += OnStartFilteringRequested;
    }

    private void OnStartFilteringRequested(FilterData data)
    {
        StartFilteringRequested?.Invoke(data);
    }
}
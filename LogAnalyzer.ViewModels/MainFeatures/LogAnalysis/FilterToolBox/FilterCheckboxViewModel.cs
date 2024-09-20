using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterCheckboxViewModel(Func<LogEntryViewModel, bool> filterFunc, string filterHeader) : ViewModelBase
{
    public Func<LogEntryViewModel, bool> FilterFunction { get; } = filterFunc;
    public string FilterHeader { get; } = filterHeader;

    private bool _checked;
    public bool Checked
    {
        get => _checked;
        set => SetProperty(ref _checked, value);
    }
}
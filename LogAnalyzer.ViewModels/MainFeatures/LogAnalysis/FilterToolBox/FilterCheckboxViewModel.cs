using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterCheckboxViewModel(object filterType, string filter) : ViewModelBase
{
    public object FilterType { get; } = filterType;
    public string FilterHeader { get; } = $"{filter} only";

    private bool _checked;
    public bool Checked
    {
        get => _checked;
        set => SetProperty(ref _checked, value);
    }
}
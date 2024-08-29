using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterCheckboxViewModel(string filter) : ViewModelBase
{
    public string FilterType { get; } = filter;
    public string FilterHeader { get; } = $"{filter} only";
   
    private bool _checked;
    public bool Checked
    {
        get => _checked;
        set => SetProperty(ref _checked, value);
    }
}
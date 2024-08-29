using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterTextboxViewModel : ViewModelBase
{
    private string _text = string.Empty;
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }
}
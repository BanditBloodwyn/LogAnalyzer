using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;

public class LogAnalysisToolPanelViewModel : ViewModelBase
{
    protected const int TEXTBOXFILTER_COUNT = 8;

    #region UI Bindings

    public ObservableCollection<string> ShowFilterStrings { get; } = [];
    public ObservableCollection<string> HideFilterStrings { get; } = [];

    private bool _showOnlyDebug;
    public bool ShowOnlyDebug
    {
        get => _showOnlyDebug;
        set => SetProperty(ref _showOnlyDebug, value);
    }

    private bool _showOnlyInfo;
    public bool ShowOnlyInfo
    {
        get => _showOnlyInfo;
        set => SetProperty(ref _showOnlyInfo, value);
    }

    private bool _showOnlyWarning;
    public bool ShowOnlyWarning
    {
        get => _showOnlyWarning;
        set => SetProperty(ref _showOnlyWarning, value);
    }

    private bool _showOnlyError;
    public bool ShowOnlyError
    {
        get => _showOnlyError;
        set => SetProperty(ref _showOnlyError, value);
    }

    private bool _showOnlyBackpack;
    public bool ShowOnlyBackpack
    {
        get => _showOnlyBackpack;
        set => SetProperty(ref _showOnlyBackpack, value);
    }

    private ICommand? _startFilterCommand;
    public ICommand StartFilterCommand => _startFilterCommand ??= new StartFilterCommand(this);


    #endregion

    public event Action<FilterData>? StartFilteringRequested;

    public LogAnalysisToolPanelViewModel()
    {
        AddTextboxes();
    }

    private void AddTextboxes()
    {
        ShowFilterStrings.Clear();
        for (int i = 0; i < TEXTBOXFILTER_COUNT - 1; i++)
            ShowFilterStrings.Add("");
        
        HideFilterStrings.Clear();
        for (int i = 0; i < TEXTBOXFILTER_COUNT - 1; i++)
            HideFilterStrings.Add("");
    }

    public void StartFiltering()
    {
        StartFilteringRequested?.Invoke(new FilterData());
    }
}
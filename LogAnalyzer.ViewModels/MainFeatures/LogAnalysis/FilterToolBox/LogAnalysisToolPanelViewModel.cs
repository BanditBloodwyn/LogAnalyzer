using Atbas.Core.Logging;
using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class LogAnalysisToolPanelViewModel : ViewModelBase
{
    protected const int TEXTBOXFILTER_COUNT = 8;

    public event Action<FilterData>? StartFilteringRequested;

    #region UI Bindings

    public ObservableCollection<FilterTextboxViewModel> ShowFilterStrings { get; } = [];
    public ObservableCollection<FilterTextboxViewModel> HideFilterStrings { get; } = [];
    public ObservableCollection<FilterCheckboxViewModel> LogTypeFilters { get; } = [];
    public ObservableCollection<FilterCheckboxViewModel> SpecialFilters { get; } = [];

    private ICommand? _startFilterCommand;
    public ICommand StartFilterCommand => _startFilterCommand ??= new StartFilterCommand(this);


    #endregion

    #region Init

    public LogAnalysisToolPanelViewModel()
    {
        AddTextboxes();
        AddLogTypeFilters();
        AddSpecialFilters();
    }

    private void AddTextboxes()
    {
        ShowFilterStrings.Clear();
        for (int i = 0; i < TEXTBOXFILTER_COUNT - 1; i++)
            ShowFilterStrings.Add(new FilterTextboxViewModel());

        HideFilterStrings.Clear();
        for (int i = 0; i < TEXTBOXFILTER_COUNT - 1; i++)
            HideFilterStrings.Add(new FilterTextboxViewModel());
    }

    private void AddLogTypeFilters()
    {
        LogTypeFilters.Add(new FilterCheckboxViewModel(log => log.LogType == MessageType.Debug, "Debug only"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(log => log.LogType == MessageType.Detail, "Detail only"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(log => log.LogType == MessageType.Info, "Info only"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(log => log.LogType == MessageType.Warning, "Warning only"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(log => log.LogType == MessageType.Error, "Error only"));
    }

    private void AddSpecialFilters()
    {
        SpecialFilters.Add(new FilterCheckboxViewModel(log => log.Source.Equals("Backpack"), "Backpack only"));
        SpecialFilters.Add(new FilterCheckboxViewModel(log => log.Message.Contains("Exception") || log.InnerMessage.Contains("Exception"), "contains Exception"));
    }

    #endregion

    public void StartFiltering()
    {
        string[] showStrings = ShowFilterStrings.Select(text => text.Text).ToArray();
        string[] hideStrings = HideFilterStrings.Select(text => text.Text).ToArray();
        FilterCheckboxViewModel[] checkboxFilters = LogTypeFilters.Union(SpecialFilters).ToArray();

        bool filterIsEmpty = showStrings.All(string.IsNullOrEmpty) &&
                             hideStrings.All(string.IsNullOrEmpty) &&
                             checkboxFilters.All(filter => !filter.Checked);

        StartFilteringRequested?.Invoke(new FilterData(
            filterIsEmpty,
            showStrings,
            hideStrings,
            checkboxFilters));
    }
}
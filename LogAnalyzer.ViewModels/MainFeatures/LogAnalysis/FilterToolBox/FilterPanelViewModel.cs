using Atbas.Core.Logging;
using CommunityToolkit.Mvvm.Input;
using LogAnalyzer.Core.ViewsModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

public class FilterPanelViewModel : ViewModelBase
{
    protected const int TEXTBOXFILTER_COUNT = 8;

    public event Action<FilterData>? StartFilteringRequested;

    public ObservableCollection<FilterTextboxViewModel> ShowFilterStrings { get; } = [];
    public ObservableCollection<FilterTextboxViewModel> HideFilterStrings { get; } = [];
    public ObservableCollection<FilterCheckboxViewModel> LogTypeFilters { get; } = [];
    public ObservableCollection<FilterCheckboxViewModel> SpecialFilters { get; } = [];

    private ICommand? _startFilterCommand;
    public ICommand StartFilterCommand => _startFilterCommand ??= new RelayCommand(StartFiltering);

    private ICommand? _resetFilterCommand;
    public ICommand ResetFilterCommand => _resetFilterCommand ??= new RelayCommand(ResetFilter);

  
    #region Init

    public FilterPanelViewModel()
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

    public void ResetFilter()
    {
        foreach (FilterTextboxViewModel filter in ShowFilterStrings)
            filter.Text = string.Empty;
        foreach (FilterTextboxViewModel filter in HideFilterStrings)
            filter.Text = string.Empty;
        foreach (FilterCheckboxViewModel filter in LogTypeFilters)
            filter.Checked = false;
        foreach (FilterCheckboxViewModel filter in SpecialFilters)
            filter.Checked = false;
    }

    public void SetToShowFilters(params string[] filters)
    {
        for (int i = 0; i < filters.Length; i++)
        {
            if (!string.IsNullOrEmpty(filters[i]))
                ShowFilterStrings[i].Text = filters[i];
        }
    }

    public void SetToHideFilters(params string[] filters)
    {
        for (int i = 0; i < filters.Length; i++)
        {
            if (!string.IsNullOrEmpty(filters[i]))
                HideFilterStrings[i].Text = filters[i];
        }
    }

    public void SetLogTypeFilters(params MessageType?[] logTypes)
    {
        foreach (var logType in logTypes)
        {
            if (logType == null)
                continue;

            foreach (FilterCheckboxViewModel filter in LogTypeFilters)
            {
                string? logTypeString = logType.ToString();
                if (string.IsNullOrEmpty(logTypeString))
                    continue;

                if (filter.FilterHeader.Contains(logTypeString))
                    filter.Checked = true;
            }
        }
    }
}
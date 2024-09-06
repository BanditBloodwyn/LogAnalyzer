﻿using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Atbas.Core.Logging;

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
        LogTypeFilters.Add(new FilterCheckboxViewModel(MessageType.Debug, "Debug"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(MessageType.Detail, "Detail"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(MessageType.Info, "Info"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(MessageType.Warning, "Warning"));
        LogTypeFilters.Add(new FilterCheckboxViewModel(MessageType.Error, "Error"));
    }

    private void AddSpecialFilters()
    {
        SpecialFilters.Add(new FilterCheckboxViewModel("Backpack", "Backpack"));
        SpecialFilters.Add(new FilterCheckboxViewModel("Exception", "Exception"));
    }

    #endregion

    public void StartFiltering()
    {
        StartFilteringRequested?.Invoke(new FilterData(
            ShowFilterStrings.Select(text => text.Text).ToArray(),
            HideFilterStrings.Select(text => text.Text).ToArray(),
            LogTypeFilters.ToDictionary(f => (MessageType)f.FilterType, f => f.Checked),
            SpecialFilters.ToDictionary(f => (string)f.FilterType, f => f.Checked)));
    }
}
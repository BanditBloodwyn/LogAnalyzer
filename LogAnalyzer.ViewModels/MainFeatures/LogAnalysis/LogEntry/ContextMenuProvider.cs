using Atbas.Core.Logging;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

public class ContextMenuProvider(FilterPanelViewModel? _filterPanelVM)
{
    public MenuItem[] ContextMenuContent(LogEntryViewModel log, int clickedColumn)
    {
        MenuItem[] _items =
        [
            CreateMenuItem("Filter by timestamp", () => UseToShowFilter(log.TimeStamp)),
            CreateMenuItem("Hide this timestamp", () => UseToHideFilter(log.TimeStamp)),
            CreateMenuItem("Filter by source", () => UseToShowFilter(log.Source)),
            CreateMenuItem("Hide this source", () => UseToHideFilter(log.Source)),
            CreateMenuItem("Filter by log type", () => FilterByType(false, log.LogType)),
            CreateMenuItem("Hide this log type", () => FilterByType(true, log.LogType)),
            new MenuItem { Header = "-", Command = null },
            CreateMenuItem("Copy to clipboard", null),
        ];

        return clickedColumn switch
        {
            0 => [_items[0], _items[1], _items[6], _items.Last()],
            1 => [_items[2], _items[3], _items[6], _items.Last()],
            _ => [_items[4], _items[5], _items[6], _items.Last()]
        };
    }

    private static MenuItem CreateMenuItem(string header, Action? action)
    {
        MenuItem item = new MenuItem();
        item.Header = header;
        item.Command = new RelayCommand(() =>
        {
            action?.Invoke();
            item.ContextMenu?.Close();
        });
        return item;
    }

    private void UseToShowFilter(params string[] stringsToShow)
    {
        if (_filterPanelVM == null)
            return;

        _filterPanelVM.ResetFilter();
        _filterPanelVM.SetToShowFilters(stringsToShow);
        _filterPanelVM.StartFiltering();
    }

    private void UseToHideFilter(params string[] stringsToHide)
    {
        if (_filterPanelVM == null)
            return;

        _filterPanelVM.ResetFilter();
        _filterPanelVM.SetToHideFilters(stringsToHide);
        _filterPanelVM.StartFiltering();
    }

    private void FilterByType(bool inverted, params MessageType?[] messageTypes)
    {
        if (_filterPanelVM == null)
            return;

        _filterPanelVM.ResetFilter();
        _filterPanelVM.SetLogTypeFilters(inverted, messageTypes);
        _filterPanelVM.StartFiltering();
    }
}
using Atbas.Core.Logging;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

public class ContextMenuProvider(LogAnalysisToolPanelViewModel? _toolPanelVM)
{
    public MenuItem[] ContextMenuContent(LogEntryViewModel log, int clickedColumn)
    {
        MenuItem[] _items =
        [
            CreateMenuItem("Filter by timestamp", () => UseToShowFilter(log.TimeStamp)),
            CreateMenuItem("Filter by source", () => UseToShowFilter(log.Source)),
            CreateMenuItem("Filter by log type", () => FilterByType(log.LogType)),
            new MenuItem { Header = "-", Command = null },
            CreateMenuItem("Copy to clipboard", null),
        ];

        return clickedColumn switch
        {
            0 => [_items[0], _items[3], _items[4]],
            1 => [_items[1], _items[3], _items[4]],
            _ => [_items[2], _items[3], _items[4]]
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
        if (_toolPanelVM == null)
            return;

        _toolPanelVM.ResetFilter();
        _toolPanelVM.SetToShowFilters(stringsToShow);
        _toolPanelVM.StartFiltering();
    }

    private void FilterByType(params MessageType?[] messageTypes)
    {
        if (_toolPanelVM == null)
            return;

        _toolPanelVM.ResetFilter();
        _toolPanelVM.SetLogTypeFilters(messageTypes);
        _toolPanelVM.StartFiltering();
    }
}
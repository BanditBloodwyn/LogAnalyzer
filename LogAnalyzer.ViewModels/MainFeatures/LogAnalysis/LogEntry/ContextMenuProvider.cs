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
            new MenuItem { Header = "Filter by timestamp", Command = new RelayCommand(() => UseToShowFilter(log.TimeStamp)) },
            new MenuItem { Header = "Filter by source", Command = new RelayCommand(() => UseToShowFilter(log.Source)) },
            new MenuItem { Header = "Filter by log type", Command = new RelayCommand(() => FilterByType(log.LogType)) },
            new MenuItem { Header = "-", Command = null },
            new MenuItem { Header = "Copy content", Command = null },
        ];

        return clickedColumn switch
        {
            0 => [_items[0], _items[3], _items[4]],
            1 => [_items[1], _items[3], _items[4]],
            _ => [_items[2], _items[3], _items[4]]
        };
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
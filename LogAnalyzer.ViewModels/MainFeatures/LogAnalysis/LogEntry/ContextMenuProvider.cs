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
            new MenuItem { Header = "Filter by timestamp", Command = new RelayCommand(() => FilterByTileStamp(log)) },
            new MenuItem { Header = "Filter by source", Command = new RelayCommand(() => FilterBySource(log)) },
            new MenuItem { Header = "Filter by log type", Command = new RelayCommand(() => FilterByType(log)) },
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

    private void FilterByTileStamp(LogEntryViewModel log)
    {
        if (_toolPanelVM == null)
            return;
    }

    private void FilterBySource(LogEntryViewModel log)
    {
        if (_toolPanelVM == null)
            return;
       
        _toolPanelVM.ResetFilter();
        _toolPanelVM.SetToShowFilters(log.Source);
        _toolPanelVM.StartFiltering();
    }

    private void FilterByType(LogEntryViewModel log)
    {
        if (_toolPanelVM == null)
            return;

        _toolPanelVM.ResetFilter();
        _toolPanelVM.SetLogTypeFilters(log.LogType);
        _toolPanelVM.StartFiltering();
    }
}
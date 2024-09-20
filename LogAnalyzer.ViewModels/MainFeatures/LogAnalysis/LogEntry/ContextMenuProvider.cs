using Avalonia.Controls;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;

public static class ContextMenuProvider
{
    public static MenuItem[] ContextMenuContent(int clickedColumn)
    {
        MenuItem[] _items =
        [
            new MenuItem { Header = "Filter by timestamp", Command = null },
            new MenuItem { Header = "Filter by source", Command = null },
            new MenuItem { Header = "Filter by log type", Command = null },
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
}
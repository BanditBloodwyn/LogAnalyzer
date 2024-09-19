using Avalonia.Controls;
using Avalonia.Input;

namespace LogAnalyzer.Core.Extentions;

public static class GridExtentions
{
    public static int GetColumnFromPosition(this Grid grid, double x)
    {
        int column = 0;
        double accumulatedWidth = 0.0;

        for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
        {
            accumulatedWidth += grid.ColumnDefinitions[i].ActualWidth;
            if (x <= accumulatedWidth)
            {
                column = i;
                break;
            }
        }
        return column;
    }
}
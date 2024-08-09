using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis;

public class LogToPaddingConverter : IMultiValueConverter
{
    private readonly List<double> _logEntryOffsets = [];

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is not int fileIndex)
            return new Thickness(0);
        if (values[1] is not int fileCount)
            return new Thickness(0);

        if (_logEntryOffsets.Count != fileCount)
        {
            _logEntryOffsets.Clear();
            _logEntryOffsets.AddRange(DistributePositions(-50, 50, fileCount));
        }

        double offset = _logEntryOffsets[fileIndex];

        return new Thickness(offset, 0, -offset, 0);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<double> DistributePositions(double min, double max, int count)
    {
        if (count < 2)
        {
            yield return 0;
            yield break;
        }

        for (int i = 0; i < count; i++)
            yield return min + (max - min) * i / (count - 1);
    }
}
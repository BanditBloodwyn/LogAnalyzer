using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;
using LogAnalyzer.Models.Data.Containers;
using System.Collections.Generic;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis;

public class LogFileOffsetToPaddingConverter : IValueConverter
{
    private readonly List<double> _logEntryOffsets = [];

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not FileAssignment fileAssignment)
            return new Thickness(0);

        if (_logEntryOffsets.Count == 0)
            _logEntryOffsets.AddRange(DistributePositions(-50, 50, fileAssignment.TotalFileCount));

        double offset = _logEntryOffsets[fileAssignment.FileIndex];

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
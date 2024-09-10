using Avalonia.Data.Converters;
using Avalonia.Media;
using LogAnalyzer.Resources;
using System;
using System.Globalization;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis.Converters;

public class MarkingToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool isMarked)
            return new SolidColorBrush(DefaultColors.Light);

        return isMarked
            ? new SolidColorBrush(DefaultColors.Success)
            : new SolidColorBrush(DefaultColors.Light);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}
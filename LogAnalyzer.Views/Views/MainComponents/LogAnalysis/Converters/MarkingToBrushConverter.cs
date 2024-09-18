using Avalonia;
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
            ? CreateGradientBrush(DefaultColors.Success)
            : new SolidColorBrush(DefaultColors.Light);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

    private static IBrush? CreateGradientBrush(Color color)
    {
        return new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            EndPoint = new RelativePoint(0.25, 0, RelativeUnit.Relative),
            GradientStops = new GradientStops
            {
                new(color, 0),
                new(DefaultColors.Light, 1)
            }
        };
    }
}
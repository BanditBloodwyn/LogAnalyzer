using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using LogAnalyzer.Resources;
using System;
using System.Globalization;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis;

public class LogTypeToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as string)?.ToLower() switch
        {
            "error" => CreateGradientBrush(DefaultColors.Danger),
            "warning" => CreateGradientBrush(DefaultColors.Warning),
            "info" => CreateGradientBrush(DefaultColors.Info),
            _ => CreateGradientBrush(DefaultColors.Accent3)
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static IBrush? CreateGradientBrush(Color color)
    {
        return new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            EndPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            GradientStops = new GradientStops
            {
                new(color, 0),
                new(color, 1)
            }
        };
    }
}
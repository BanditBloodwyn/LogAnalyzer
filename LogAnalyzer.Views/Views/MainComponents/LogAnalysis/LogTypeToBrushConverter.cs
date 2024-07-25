using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using LogAnalyzer.Core.Extentions;
using System;
using System.Globalization;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis;

public class LogTypeToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as string)?.ToLower() switch
        {
            "error" => CreateGradientBrush(Colors.LightPink),
            "warning" => CreateGradientBrush(Colors.LightYellow),
            "info" => CreateGradientBrush(Colors.LightBlue),
            _ => new SolidColorBrush(Colors.Transparent)
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static IBrush CreateGradientBrush(Color color)
    {
        return new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            EndPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
            GradientStops = new GradientStops
            {
                new(color.WithOpacity(0.1), 0),
                new(color.WithOpacity(0.4), 1)
            }
        };
    }
}
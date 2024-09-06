using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using LogAnalyzer.Resources;
using System;
using System.Globalization;
using Atbas.Core.Logging;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis.Converters;

public class LogTypeToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value is MessageType type ? type : MessageType.Debug) switch
        {
            MessageType.Error => CreateGradientBrush(DefaultColors.Danger),
            MessageType.Warning => CreateGradientBrush(DefaultColors.Warning),
            MessageType.Info => CreateGradientBrush(DefaultColors.Info),
            MessageType.Detail => CreateGradientBrush(DefaultColors.Success),
            MessageType.Debug => CreateGradientBrush(DefaultColors.Accent3),
            _ => throw new ArgumentOutOfRangeException()
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
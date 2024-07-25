using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis;

public class LogInnerMessageHeightConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? logString = value as string;
        
        if (string.IsNullOrEmpty(logString))
            return 0;
        
        int lineCount = logString.Split("\r").Length;
        
        return 6.5f + lineCount * 13.5f;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}
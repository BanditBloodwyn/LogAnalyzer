using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Resources;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis.LogEntryElement;

public partial class LogEntryControl : UserControl
{
    public static readonly StyledProperty<bool> IsExpandedProperty =
        AvaloniaProperty.Register<LogEntryControl, bool>(nameof(IsExpanded), defaultValue: false);

    private bool HasInnerMessage
    {
        get
        {
            if (DataContext is LogEntry logEntry)
                return !string.IsNullOrEmpty(logEntry.InnerMessage);
            return false;
        }
    }
    private bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public LogEntryControl()
    {
        InitializeComponent();
        

        img_HasInnerMessage.Source = DefaultIcons.LogInnerMessageIcon;
    }

    private void OnInitialized(object? sender, EventArgs e)
    {
        UpdateHeight();
        UpdateUI();
    }

    private void Pnl_Header_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!HasInnerMessage)
            return;

        IsExpanded = !IsExpanded;
        
        UpdateHeight();
        UpdateUI();
    }

    private void UpdateHeight()
    {
        Height = IsExpanded
            ? lbl_InnerMessage.Height
            : 30;

        Design.SetHeight(this, Height);
    }

    private void UpdateUI()
    {
        lbl_Header_Message.IsVisible = !IsExpanded;
        img_HasInnerMessage.IsVisible = HasInnerMessage;
    }
}
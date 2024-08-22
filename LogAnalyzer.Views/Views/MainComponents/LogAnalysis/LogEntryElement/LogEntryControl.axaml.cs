using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.Resources;
using System;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis.LogEntryElement;

public partial class LogEntryControl : UserControl
{
    private LogEntry? _logEntry;

    public static readonly StyledProperty<bool> IsExpandedProperty =
        AvaloniaProperty.Register<LogEntryControl, bool>(nameof(IsExpanded), defaultValue: false);
    private bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public LogEntryControl()
    {
        InitializeComponent();

        img_HasInnerMessage.Source = DefaultIcons.LogInnerMessageIcon;
        img_RepoDurationWarning.Source = DefaultIcons.LogLongRepoInteraction;
        img_RepoDurationCritical.Source = DefaultIcons.LogVeryLongRepoInteraction;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is not LogEntry logEntry)
            return;

        _logEntry = logEntry;

        InitIcons();

        UpdateHeight();
        UpdateUI();
    }

    private void InitIcons()
    {
        img_HasInnerMessage.IsVisible = _logEntry?.HasInnerMessage ?? false;
        img_RepoDurationWarning.IsVisible = _logEntry?.RepositoryInteractionInformation?.HasDurationWarning ?? false;
        img_RepoDurationCritical.IsVisible = _logEntry?.RepositoryInteractionInformation?.HasCriticalDuration ?? false;
    }

    private void Pnl_Header_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!_logEntry.HasValue)
            return;

        if (!_logEntry.Value.HasInnerMessage)
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
    }
}
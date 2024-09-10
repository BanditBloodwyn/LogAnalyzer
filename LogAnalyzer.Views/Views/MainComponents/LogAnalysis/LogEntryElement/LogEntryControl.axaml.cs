using Avalonia.Controls;
using Avalonia.Input;
using LogAnalyzer.Resources;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using System;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis.LogEntryElement;

public partial class LogEntryControl : UserControl
{
    private LogEntryViewModel? _logEntry;
    private bool _isExpanded;

    private bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            _isExpanded = value;
            UpdateUI();
        }
    }

    public LogEntryControl()
    {
        InitializeComponent();

        img_HasInnerMessage.Source = DefaultIcons.LogInnerMessageIcon;
        img_RepoDurationWarning.Source = DefaultIcons.LogLongRepoInteraction;
        img_RepoDurationCritical.Source = DefaultIcons.LogVeryLongRepoInteraction;

        PointerEntered += OnPointerEntered;
        PointerExited += OnPointerExited;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is not LogEntryViewModel logEntry)
            return;

        _logEntry = logEntry;

        img_HasInnerMessage.IsVisible = _logEntry?.HasInnerMessage ?? false;
        img_RepoDurationWarning.IsVisible = _logEntry?.RepositoryInteractionInformation?.HasDurationWarning ?? false;
        img_RepoDurationCritical.IsVisible = _logEntry?.RepositoryInteractionInformation?.HasCriticalDuration ?? false;

        UpdateUI();
    }

    private void UpdateUI()
    {
        Height = IsExpanded
            ? lbl_InnerMessage.Height
            : 30;

        lbl_Header_Message.IsVisible = !IsExpanded;
    }

    private void Pnl_Header_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!_logEntry?.HasInnerMessage ?? false)
            return;

        IsExpanded = !IsExpanded;
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e)
    {
        _logEntry?.OnPointerEntered();
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        _logEntry?.OnPointerExited();
    }
}
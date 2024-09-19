using Avalonia.Controls;
using Avalonia.Input;
using LogAnalyzer.Core.Extentions;
using LogAnalyzer.Resources;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;
using System;

namespace LogAnalyzer.Views.Views.MainComponents.LogAnalysis.LogEntryElement;

public partial class LogEntryControl : UserControl
{
    private LogEntryViewModel? _viewModel;
    private bool _isExpanded;
    private int _currentColumn = -1;

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
        PointerMoved += OnPointerMoved;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is not LogEntryViewModel logEntry)
            return;

        _viewModel = logEntry;

        img_HasInnerMessage.IsVisible = _viewModel?.HasInnerMessage ?? false;
        img_RepoDurationWarning.IsVisible = _viewModel?.RepositoryInteractionInformation?.HasDurationWarning ?? false;
        img_RepoDurationCritical.IsVisible = _viewModel?.RepositoryInteractionInformation?.HasCriticalDuration ?? false;

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
        PointerPointProperties properties = e.GetCurrentPoint(sender as Control).Properties;

        if (properties.IsLeftButtonPressed)
        {
            if (!_viewModel?.HasInnerMessage ?? false)
                return;

            IsExpanded = !IsExpanded;
        }
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e) => _viewModel?.OnPointerEntered();

    private void OnPointerExited(object? sender, PointerEventArgs e) => _viewModel?.OnPointerExited();

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        int column = grid_Header.GetColumnFromPosition(e.GetPosition(grid_Header).X);

        if (_currentColumn != column)
            _viewModel?.UpdateContextMenuContent(column);

        _currentColumn = column;
    }
}
using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.Commands;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Timers;

namespace LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.MergedView;

public class MergedLogPanelViewModel : LogPanelBaseViewModel
{
    private const int UPDATEINTERVALMS = 500;

    private readonly Stopwatch _stopwatch = new Stopwatch();
    private readonly System.Timers.Timer _updateTimer;
    private bool _updatePending;
    private FilterData? _filter;

    private List<LogEntryViewModel> _logEntries = [];
    public List<LogEntryViewModel> LogEntries
    {
        get => _logEntries;
        set => SetProperty(ref _logEntries, value);
    }

    public List<LogEntryViewModel> FilteredList => _logEntries
        .Where(logEntry => FilterBuilder.BuildFilter(_filter, logEntry))
        .ToList();

    public MergedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand commandFactory)
        : base(commandFactory)
    {
        Cache.LogEntries.CollectionChanged += OnLogEntriesChanged;

        _updateTimer = new System.Timers.Timer(UPDATEINTERVALMS);
        _updateTimer.Elapsed += OnUpdateTimerElapsed;
        _stopwatch.Start();
    }

    protected override void Reset()
    {
        LogEntries.Clear();
    }

    public override void SetFilter(FilterData filter)
    {
        _filter = filter;
        OnPropertyChanged(nameof(FilteredList));
    }

    private void OnLogEntriesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (_stopwatch.ElapsedMilliseconds >= UPDATEINTERVALMS)
        {
            UpdateLogEntries();
            _stopwatch.Restart();
        }
        else
        {
            _updatePending = true;

            if (!_updateTimer.Enabled)
                _updateTimer.Start();
        }
    }

    private void OnUpdateTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _updateTimer.Stop();

        if (_updatePending)
        {
            UpdateLogEntries();

            _updatePending = false;
            _stopwatch.Restart();
        }
    }

    private void UpdateLogEntries()
    {
        try
        {
            List<LogEntry> cacheSnapshot;
            lock (Cache.LogEntries)
                cacheSnapshot = [.. Cache.LogEntries];

            List<long> cacheIndeces = cacheSnapshot.Select(log => log.LogIndex).ToList();
            List<long> vmIndeces = LogEntries.Select(log => log.LogIndex).ToList();
            List<long> newIndeces = cacheIndeces.Except(vmIndeces).ToList();
            List<LogEntry> newEntries = cacheSnapshot.Where(log => newIndeces.Contains(log.LogIndex)).ToList();

            if (newEntries.Count != 0)
            {
                LogEntries = [.. LogEntries.Concat(CreateNewLogEntryVMs(newEntries))];
                LogEntries.Sort(TimeComparison);
                OnPropertyChanged(nameof(LogEntries));
                OnPropertyChanged(nameof(FilteredList));
            }
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    private static int TimeComparison(LogEntryViewModel item1, LogEntryViewModel item2)
    {
        if (!DateTime.TryParse(item1.TimeStamp, out DateTime timeStamp1) ||
            !DateTime.TryParse(item2.TimeStamp, out DateTime timeStamp2))
            return 0;

        return timeStamp1.CompareTo(timeStamp2);
    }

    private static IEnumerable<LogEntryViewModel> CreateNewLogEntryVMs(IEnumerable<LogEntry> newEntries)
    {
        foreach (LogEntry newEntry in newEntries)
        {
            if(newEntry.LogMessage == null)
                continue;

            yield return new LogEntryViewModel(newEntry);
        }
    }
}
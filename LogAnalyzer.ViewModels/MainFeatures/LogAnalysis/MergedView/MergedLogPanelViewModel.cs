using LogAnalyzer.Models.Data.Containers;
using LogAnalyzer.ViewModels.Commands;
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

    private List<LogEntryViewModel> _logEntries = [];
    public List<LogEntryViewModel> LogEntries
    {
        get => _logEntries;
        set => SetProperty(ref _logEntries, value);
    }

    public MergedLogPanelViewModel(CommandFactory.CreateLogAnalyzeCommand commandFactory)
        : base(commandFactory)
    {
        Cache.LogEntries.CollectionChanged += OnLogEntriesChanged;

        _updateTimer = new System.Timers.Timer(UPDATEINTERVALMS);
        _updateTimer.Elapsed += OnUpdateTimerElapsed;
        _stopwatch.Start();
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
        List<LogEntry> cacheSnapshot;
        lock (Cache.LogEntries)
            cacheSnapshot = [.. Cache.LogEntries];

        List<long> cacheIndeces = cacheSnapshot.Select(log => log.index).ToList();
        List<long> vmIndeces = LogEntries.Select(log => log.Index).ToList();
        List<long> newIndeces = cacheIndeces.Except(vmIndeces).ToList();
        List<LogEntry> newEntries = cacheSnapshot.Where(log => newIndeces.Contains(log.index)).ToList();

        if (newEntries.Count != 0)
        {
            LogEntries = [.. LogEntries.Concat(CreateNewLogEntryVMs(newEntries))];
            LogEntries.Sort((item1, item2) => DateTime.Parse(item1.TimeStamp).CompareTo(DateTime.Parse(item2.TimeStamp)));
            OnPropertyChanged(nameof(LogEntries));
        }
    }

    private static IEnumerable<LogEntryViewModel> CreateNewLogEntryVMs(IEnumerable<LogEntry> newEntries)
    {
        foreach (LogEntry newEntry in newEntries)
            yield return new LogEntryViewModel(newEntry);
    }
}
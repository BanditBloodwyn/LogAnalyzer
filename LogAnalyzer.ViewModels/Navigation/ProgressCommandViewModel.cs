using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.BaseTypes.Commands;

namespace LogAnalyzer.ViewModels.Navigation;

public class ProgressCommandViewModel(ProgressCommand _command)
    : ViewModelBase
{
    public event Action<ProgressCommandViewModel>? ProgressFinished;

    public string Name => _command.Name;

    private int _percentsDone;
    public int PercentsDone
    {
        get => _percentsDone;
        set
        {
            _percentsDone = value;
            OnPropertyChanged();
        }
    }

    private string _currentTaskText = string.Empty;
    public string CurrentTaskText
    {
        get => _currentTaskText;
        set
        {
            _currentTaskText = value;
            OnPropertyChanged();
        }
    }

    public async void Start()
    {
        IProgress<int> percents = new Progress<int>(perc => PercentsDone = perc);
        IProgress<string> currentTask = new Progress<string>(text => CurrentTaskText = text);

        _command.PercentsProgress = percents;
        _command.MessageProgress = currentTask;

        await _command.Execute();
        ProgressFinished?.Invoke(this);
    }
}
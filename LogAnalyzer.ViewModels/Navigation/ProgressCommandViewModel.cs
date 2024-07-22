using LogAnalyzer.Core.ViewsModels;
using LogAnalyzer.Models.Data.BaseTypes.Commands;

namespace LogAnalyzer.ViewModels.Navigation;

public class ProgressCommandViewModel(ProgressCommand _command)
    : ViewModelBase
{
    public event Action<ProgressCommandViewModel>? ProgressFinished;

    private string _name = _command.Name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private int _percentsDone;
    public int PercentsDone
    {
        get => _percentsDone;
        set => SetProperty(ref _percentsDone, value);
    }

    private string _currentTaskText = string.Empty;
    public string CurrentTaskText
    {
        get => _currentTaskText;
        set => SetProperty(ref _currentTaskText, value);
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
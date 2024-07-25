using LogAnalyzer.Models.LogAnalysis;
using LogAnalyzer.ViewModels.Navigation;

namespace LogAnalyzer.ViewModels.Design.Navigation;

public class ProgressCommandDesignViewModel : ProgressCommandViewModel
{
    public ProgressCommandDesignViewModel() 
        : base(new LogAnalyzeCommand(null!, null!))
    {
        Name = "Analyze log file";
        PercentsDone = 30;
        CurrentTaskText = "Trying to get the command queue done";
    }
}
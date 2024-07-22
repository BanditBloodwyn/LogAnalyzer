using LogAnalyzer.Models.Data.BaseTypes.Commands;

namespace LogAnalyzer.ViewModels.Commands.LogAnalysis;

public class LogAnalyzeCommand() : ProgressCommand("Analyze log file")
{
    public override async Task Execute()
    {
        for (int i = 0; i < 100; i += 10)
        {
            await Task.Delay(500);
            PercentsProgress?.Report(i);
            MessageProgress?.Report("Trying to get the command queue done");
        }
    }
}
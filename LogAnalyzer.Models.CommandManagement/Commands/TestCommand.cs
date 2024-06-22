using LogAnalyzer.Models.Data.Interfaces;

namespace LogAnalyzer.Models.CommandQueue.Commands;

public class TestCommand : IQueuedCommand
{
    public string Name => "Test Command";
    
    public async Task Execute()
    {
        await Task.Delay(1000);
    }
}
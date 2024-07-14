using LogAnalyzer.Models.Data.Interfaces;

namespace LogAnalyzer.Models.CommandQueue.Commands;

public class InvokerCommand(string _name, Func<Task> _task) 
    : IQueuedCommand
{
    public string Name { get; } = _name;
    
    public async Task Execute()
    { 
        await _task();
    }
}
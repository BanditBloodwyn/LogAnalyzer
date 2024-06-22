namespace LogAnalyzer.Models.Data.Interfaces;

// strategy pattern
public interface IQueuedCommand
{
    public string Name { get; }

    public Task Execute();
}
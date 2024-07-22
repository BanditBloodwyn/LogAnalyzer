using LogAnalyzer.Models.Data.Interfaces;

namespace LogAnalyzer.Models.Data.BaseTypes.Commands;

public abstract class ProgressCommand(string name) : IQueuedCommand
{
    public string Name { get; } = name;

    public IProgress<int>? PercentsProgress { get; set; }
    public IProgress<string>? MessageProgress { get; set; }

    public abstract Task Execute();
}
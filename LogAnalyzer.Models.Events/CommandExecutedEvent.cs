using LogAnalyzer.Core.EventBus;

namespace LogAnalyzer.Models.Events;

public struct CommandExecutedEvent(string[] commandsInQueue) : IEvent
{
    public string[] CommandsInQueue = commandsInQueue;
}
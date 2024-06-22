using LogAnalyzer.Core.EventBus;

namespace LogAnalyzer.Models.Events;

public struct CommandExecutionStartedEvent(string commmandName) : IEvent
{
    public string CommmandName = commmandName;
}
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Models.Data.Interfaces;

namespace LogAnalyzer.Models.Events;

public struct AddNewQueuedCommandEvent(IQueuedCommand command) : IEvent
{
    public IQueuedCommand Command = command;
}
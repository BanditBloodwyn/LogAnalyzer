using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Models.Data.BaseTypes.Commands;

namespace LogAnalyzer.Models.Events;

public struct AddNewProgressCommandEvent(ProgressCommand command) : IEvent
{
    public ProgressCommand Command = command;
}
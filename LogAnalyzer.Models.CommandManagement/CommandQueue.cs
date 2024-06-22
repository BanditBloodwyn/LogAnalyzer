using System.Diagnostics;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Models.Data.Interfaces;
using LogAnalyzer.Models.Events;

namespace LogAnalyzer.Models.CommandQueue;

public class CommandQueue
{
    private readonly Queue<IQueuedCommand> _queue = new();

    private bool _isProcessing;

    public CommandQueue()
    {
        EventBinding<AddNewQueuedCommandEvent> addNewCommandEventBinding = new(EnqueueCommand);
        EventBus<AddNewQueuedCommandEvent>.Register(addNewCommandEventBinding);
    }

    private void EnqueueCommand(AddNewQueuedCommandEvent @event)
    {
        _queue.Enqueue(@event.Command);
        Debug.WriteLine($"Command added to queue. New length: {_queue.Count}");

        if (!_isProcessing)
            _ = ProcessCommands();
    }

    private async Task ProcessCommands()
    {
        _isProcessing = true;

        while (_queue.Count > 0)
        {
            IQueuedCommand command = _queue.Dequeue();
            await Task.Run(command.Execute);
            Debug.WriteLine($"Command executed. Commands left: {_queue.Count}");
        }

        _isProcessing = false;
    }
}
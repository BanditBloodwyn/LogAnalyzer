using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Models.Data.Interfaces;
using LogAnalyzer.Models.Events;
using System.Collections.Concurrent;

namespace LogAnalyzer.Models.CommandQueue;

public class CommandQueue
{
    private readonly ConcurrentQueue<IQueuedCommand> _commands = new();
    private readonly SemaphoreSlim _signal = new(0);

    public CommandQueue()
    {
        Task.Run(ProcessCommandsAsync);

        EventBinding<AddNewQueuedCommandEvent> addNewCommandEventBinding = new(EnqueueCommand);
        EventBus<AddNewQueuedCommandEvent>.Register(addNewCommandEventBinding);
    }

    public void EnqueueCommand(AddNewQueuedCommandEvent @event)
    {
        _commands.Enqueue(@event.Command);
        _signal.Release();
        Console.WriteLine($"Command added to queue. New length: {_commands.Count}");
    }

    private async Task ProcessCommandsAsync()
    {
        while (true)
        {
            await _signal.WaitAsync();

            if (_commands.TryDequeue(out IQueuedCommand? command))
            {
                try
                {
                    await Task.Run(command.Execute);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while executing command '{command.Name}': {ex.Message}");
                }
            }
        }
    }

    public Task WaitForAllCommandsAsync()
    {
        return Task.Run(async () =>
        {
            while (!_commands.IsEmpty || _signal.CurrentCount > 0)
            {
                await Task.Delay(100);
            }
        });
    }
}
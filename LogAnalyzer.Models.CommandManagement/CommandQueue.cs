using LogAnalyzer.Models.Data.BaseTypes.Commands;
using System.Collections.Concurrent;

namespace LogAnalyzer.Models.CommandQueue;

public class CommandQueue
{
    private readonly ConcurrentQueue<ProgressCommand> _commands = new();
    private readonly SemaphoreSlim _signal = new(0);

    public event Action<ProgressCommand>? CommandFinished;

    public CommandQueue()
    {
        Task.Run(ProcessCommandsAsync);
    }

    public void EnqueueCommand(ProgressCommand command)
    {
        _commands.Enqueue(command);
        _signal.Release();
        Console.WriteLine($"Command added to queue. New length: {_commands.Count}");
    }

    private async Task ProcessCommandsAsync()
    {
        while (true)
        {
            await _signal.WaitAsync();

            if (_commands.TryDequeue(out ProgressCommand? command))
            {
                try
                {
                    await Task.Run(command.Execute);
                    CommandFinished?.Invoke(command);
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
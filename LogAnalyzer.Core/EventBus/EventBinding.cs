﻿namespace LogAnalyzer.Core.EventBus;

public class EventBinding<T> : IEventBinding<T>
    where T : IEvent
{
    private Action<T> _onEvent = static _ => { };
    private Action _onEventNoArgs = static () => { };

    Action<T> IEventBinding<T>.OnEvent
    {
        get => _onEvent; 
        set => _onEvent = value;
    }

    Action IEventBinding<T>.OnEventNoArgs
    {
        get => _onEventNoArgs;
        set => _onEventNoArgs = value;
    }

    public EventBinding(Action<T> onEvent) => _onEvent = onEvent;
    public EventBinding(Action onEventNoArgs) => _onEventNoArgs = onEventNoArgs;
}
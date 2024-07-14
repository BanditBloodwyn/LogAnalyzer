using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.EventBus;

namespace LogAnalyzer.Models.Events;

public struct ChangeOpenedModuleEvent(MainViewComponent module) : IEvent
{
    public MainViewComponent Module = module;
}

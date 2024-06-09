using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.Modules;

namespace LogAnalyzer.Models.Events;

public struct ChangeOpenedModuleEvent(MainViewModule module) : IEvent
{
    public MainViewModule Module = module;
}

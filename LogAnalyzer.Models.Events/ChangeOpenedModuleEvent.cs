using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.Models.Events;

public struct ChangeOpenedModuleEvent(MainModuleViewModelBase module) : IEvent
{
    public MainModuleViewModelBase Module = module;
}
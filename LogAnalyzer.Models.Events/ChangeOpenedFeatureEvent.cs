using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.ViewsModels;

namespace LogAnalyzer.Models.Events;

public struct ChangeOpenedFeatureEvent(MainFeatureViewModelBase feature) : IEvent
{
    public MainFeatureViewModelBase Feature = feature;
}
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Models.CommandQueue;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.BackendExtentions;

public class CommandInvokationComponent : ComponentBase,
    IDependencyInjectionComponent
{
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddSingleton<CommandQueue>();
    }
}
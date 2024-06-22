using LogAnalyzer.Core.Modules;
using LogAnalyzer.Core.Modules.Interfaces;
using LogAnalyzer.Models.CommandQueue;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Modules.BackendExtentions;

public class CommandInvokationModule : ModuleBase,
    IDependencyInjectionModule, IReactToDIModule
{
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddSingleton<CommandQueue>();
    }

    public void OnDIFinished()
    {
        GetDependency<CommandQueue>();
    }
}
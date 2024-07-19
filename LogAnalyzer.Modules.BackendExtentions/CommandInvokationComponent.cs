using System.Windows.Input;
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Models.CommandQueue;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.BackendExtentions;

public class CommandInvokationComponent : ComponentBase,
    IDependencyInjectionComponent, IReactToDIComponent, ICommandCreator
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

    public ICommand? CreateCommand(Type commandType)
    {
        return GetDependency(commandType) as ICommand;
    }
}
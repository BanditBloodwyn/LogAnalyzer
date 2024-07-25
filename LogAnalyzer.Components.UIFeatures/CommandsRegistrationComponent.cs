using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Models.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.Commands;
using LogAnalyzer.ViewModels.Commands.LogAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.UIFeatures;

public class CommandsRegistrationComponent : ComponentBase,
    IDependencyInjectionComponent
{
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddTransient<LogAnalyzeCommand>()

            .AddTransient<CommandFactory.CreateLogAnalyzeCommand>(static serviceProvider =>
                serviceProvider.GetRequiredService<LogAnalyzeCommand>);
    }
}
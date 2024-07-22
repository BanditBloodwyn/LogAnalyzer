using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Models.Strategies.LogStringFinding;
using LogAnalyzer.Models.Strategies.LogStringParsing;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.MainNavigationFeatures;

public class LogAnalysisComponent : ComponentBase,
    IDependencyInjectionComponent
{
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddTransient<ILogStringFindingStrategy, ClassicATBASLogStringFinding>()
            .AddTransient<ILogStringParsingStrategy, ClassicATBASLogStringParsing>()
            .AddSingleton<LogAnalysisModuleViewModel>();
    }
}
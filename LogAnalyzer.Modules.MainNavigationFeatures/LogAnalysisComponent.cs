using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Models.Modules.LogAnalysis;
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
            .AddSingleton<ILogStringFindingStrategy, ClassicATBASLogStringFinding>()
            .AddSingleton<ILogStringParsingStrategy, ClassicATBASLogStringParsing>()
            .AddSingleton<ILogAnalysisModel, LogAnalysisModel>()
            .AddSingleton<LogAnalysisModuleViewModel>();
    }
}
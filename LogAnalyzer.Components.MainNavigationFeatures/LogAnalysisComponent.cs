﻿using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Models.Strategies.RepositoryInteractionInformationExtraction;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.MainNavigationFeatures;

public class LogAnalysisComponent : ComponentBase,
    IDependencyInjectionComponent
{
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddSingleton<ICommIdExtractor, ClassicATBASCommIdExtractor>()
            .AddSingleton<ICommDurationExtractor, ClassicATBASCommDurationExtractor>()
            .AddSingleton<ICommResultCountExtractor, ClassicATBASCommResultCountExtractor>()
            .AddSingleton<IRepositoryInteractionInformationExtractor, ClassicATBASRepositoryInteractionInformationExtractor>();
    }
}
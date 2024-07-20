﻿using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.ViewModels;
using LogAnalyzer.ViewModels.Modules;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;
using LogAnalyzer.ViewModels.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.UIFeatures
{
    public class ViewModelsRegistrationComponent : ComponentBase,
        IDependencyInjectionComponent
    {
        public void RegisterDependencies(IServiceCollection service)
        {
            service
                .AddTransient<MainViewModel>()
                .AddTransient<MainNavigationViewModel>()
                .AddTransient<ModuleButtonsPanelViewModel>()
                .AddTransient<CommandsPanelViewModel>()
                
                .AddTransient<LogAnalysisModuleViewModel>()
                .AddTransient<SettingsModuleViewModel>();
        }
    }
}

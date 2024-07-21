using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.ViewModels;
using LogAnalyzer.ViewModels.Modules;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;
using LogAnalyzer.ViewModels.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LogAnalyzer.Components.UIFeatures
{
    public class ViewModelsRegistrationComponent : ComponentBase,
        IDependencyInjectionComponent
    {
        public void RegisterDependencies(IServiceCollection service)
        {
            service
                .AddTransient<ViewModelFactory.CreateLogPanel>(static serviceProvider => serviceProvider.GetRequiredService<LogPanelViewModel>)
                .AddTransient<MainViewModel>()
                .AddTransient<MainNavigationViewModel>()
                .AddTransient<ModuleButtonsPanelViewModel>()
                .AddTransient<CommandsPanelViewModel>()
                
                .AddTransient<LogAnalysisModuleViewModel>()
                .AddTransient<LogPanelViewModel>()
                .AddTransient<SettingsModuleViewModel>();
        }
    }
}

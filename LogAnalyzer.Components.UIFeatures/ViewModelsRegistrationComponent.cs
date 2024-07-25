using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.ViewModels;
using LogAnalyzer.ViewModels.MainComponents;
using LogAnalyzer.ViewModels.MainComponents.LogAnalysis;
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
                .AddTransient<LogPanelViewModel>()
                .AddTransient<SettingsModuleViewModel>()

                .AddTransient<ViewModelFactory.CreateLogPanel>(static serviceProvider =>
                    serviceProvider.GetRequiredService<LogPanelViewModel>);

        }
    }
}

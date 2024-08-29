using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.ViewModels;
using LogAnalyzer.ViewModels.MainFeatures;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.MergedView;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.SplittedView;
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
                .AddTransient<FeatureButtonsPanelViewModel>()
                .AddTransient<CommandsPanelViewModel>()

                .AddSingleton<LogAnalysisMainViewModel>()
                .AddTransient<SettingsMainViewModel>()
                
                .AddSingleton<LogAnalysisToolPanelViewModel>()
                .AddTransient<SplittedLogPanelViewModel>()
                .AddTransient<MergedLogPanelViewModel>()
                .AddTransient<LogPanelViewModel>()
                .AddTransient<ViewModelFactory.CreateLogPanel>(static serviceProvider =>
                    serviceProvider.GetRequiredService<LogPanelViewModel>);

        }
    }
}

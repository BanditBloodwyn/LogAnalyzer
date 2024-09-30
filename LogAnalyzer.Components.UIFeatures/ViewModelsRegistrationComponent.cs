using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.ViewModels;
using LogAnalyzer.ViewModels.MainFeatures;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.FilterToolBox;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis.LogEntry;
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
                .AddSingleton<MainViewModel>()
                .AddSingleton<MainNavigationViewModel>()
                .AddSingleton(provider => new FeatureButtonsPanelViewModel(
                        provider.GetRequiredService<LogAnalysisMainViewModel>(), 
                        provider.GetRequiredService<SettingsMainViewModel>()))
                .AddSingleton<CommandsPanelViewModel>()

                .AddSingleton<LogAnalysisMainViewModel>()
                .AddTransient<SettingsMainViewModel>()
                
                .AddSingleton<SearchPanelViewModel>()
                .AddSingleton<FilterPanelViewModel>()
                .AddSingleton<LogAnalysisToolPanelViewModel>()
                .AddSingleton<SplittedLogPanelViewModel>()
                .AddSingleton<MergedLogPanelViewModel>()
                .AddTransient<LogPanelViewModel>()
                .AddTransient<ViewModelFactory.CreateLogPanel>(static serviceProvider =>
                    serviceProvider.GetRequiredService<LogPanelViewModel>)
                
                .AddSingleton<ContextMenuProvider>();
        }
    }
}

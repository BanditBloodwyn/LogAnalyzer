using Avalonia.Controls;
using Avalonia.Media.Imaging;
using LogAnalyzer.Core.Modules;
using LogAnalyzer.Core.Modules.Interfaces;
using LogAnalyzer.Models.Modules.LogAnalysis;
using LogAnalyzer.Resources;
using LogAnalyzer.ViewModels.Modules.LogAnalysis;
using LogAnalyzer.Views.Views.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Modules.MainNavigationFeatures;

public class LogAnalysisModule : MainViewModule,
    IDependencyInjectionModule
{
    public override int NavigationIndex => 0;
    public override string ModuleHeader => "Log Analysis";
    public override Bitmap ModuleIcon => DefaultIcons.LogAnalysisIcon;

    public override UserControl GetView()
    {
        LogAnalysisModuleView view = new();
        view.DataContext = GetDependency<LogAnalysisModuleViewModel>();
        return view;
    }

    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddSingleton<ILogAnalysisModel, LogAnalysisModel>()
            .AddSingleton<LogAnalysisModuleViewModel>();
    }
}
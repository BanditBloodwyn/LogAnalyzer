using Avalonia.Controls;
using Avalonia.Media.Imaging;
using LogAnalyzer.Core.Modules;
using LogAnalyzer.Core.Modules.Interfaces;
using LogAnalyzer.Models.Modules;
using LogAnalyzer.Resources;
using LogAnalyzer.ViewModels.Modules;
using LogAnalyzer.Views.Views.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Modules.MainNavigationFeatures;

public class SettingsModule : MainViewModule,
    IDependencyInjectionModule
{
    public override int NavigationIndex => 3;
    public override string ModuleHeader => "Settings";
    public override Bitmap ModuleIcon => DefaultIcons.SettingsIcon;

    public override UserControl GetView()
    {
        SettingsModuleView view = new();
        view.DataContext = new SettingsModuleViewModel(GetDependency<SettingsModel>());
        return view;
    }
    
    public void RegisterDependencies(IServiceCollection service)
    {
        service
            .AddSingleton<SettingsModel>();
    }
}
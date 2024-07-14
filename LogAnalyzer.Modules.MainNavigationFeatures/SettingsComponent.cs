using Avalonia.Controls;
using Avalonia.Media.Imaging;
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.Components.Interfaces;
using LogAnalyzer.Models.Modules;
using LogAnalyzer.Resources;
using LogAnalyzer.ViewModels.Modules;
using LogAnalyzer.Views.Views.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Components.MainNavigationFeatures;

public class SettingsComponent : MainViewComponent,
    IDependencyInjectionComponent
{
    public override int NavigationIndex => 1;
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
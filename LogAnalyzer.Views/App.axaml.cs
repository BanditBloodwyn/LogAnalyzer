using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using LogAnalyzer.Core.Components;
using LogAnalyzer.Core.UI;
using LogAnalyzer.Models.Framework;
using LogAnalyzer.ViewModels;
using LogAnalyzer.Views.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Views;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        this.AttachDevTools();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);
        
        IServiceCollection services = new ServiceCollection();

        ComponentInitializer.InitializeComponents(services);

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(serviceProvider)
            };
            TopLevelContext.Initialize(TopLevel.GetTopLevel(desktop.MainWindow));
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(serviceProvider)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

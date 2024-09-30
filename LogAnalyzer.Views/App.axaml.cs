using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.UI;
using LogAnalyzer.Models.Events;
using LogAnalyzer.Models.Framework;
using LogAnalyzer.ViewModels;
using LogAnalyzer.ViewModels.MainFeatures.LogAnalysis;
using LogAnalyzer.Views.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileInfo = LogAnalyzer.Models.Data.Containers.FileInfo;

namespace LogAnalyzer.Views;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

#if(DEBUG)
        this.AttachDevTools();
#endif
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
                DataContext = serviceProvider.GetRequiredService<MainViewModel>()
            };
            TopLevelContext.Initialize(TopLevel.GetTopLevel(desktop.MainWindow));
            OpenFiles(desktop, serviceProvider);
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = serviceProvider.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void OpenFiles(IClassicDesktopStyleApplicationLifetime desktop, IServiceProvider serviceProvider)
    {
        if (!(desktop.Args?.Length > 0)) 
            return;

        LogAnalysisMainViewModel logAnalysis = serviceProvider.GetRequiredService<LogAnalysisMainViewModel>();
        EventBus<ChangeOpenedFeatureEvent>.Raise(new ChangeOpenedFeatureEvent(logAnalysis));
        logAnalysis.LogPanel.OpenFiles(GetFileInfos(desktop.Args).ToArray());
    }

    private static IEnumerable<FileInfo> GetFileInfos(string[] args)
    {
        for (var index = 0; index < args.Length; index++)
        {
            string arg = args[index];
            if (!File.Exists(arg))
                continue;

            string fullPath = Path.GetFullPath(arg);
            string fileName = Path.GetFileName(arg);
            Uri uri = new Uri(fullPath);
            yield return new FileInfo(index, fileName, uri.LocalPath, uri.AbsolutePath);
        }
    }
}

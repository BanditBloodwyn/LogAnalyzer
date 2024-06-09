using LogAnalyzer.Core;
using LogAnalyzer.Core.Modules;
using LogAnalyzer.Core.Modules.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Models.Framework;

public class ModuleInitializer
{
    public static ModuleBase[] InitializeModules()
    {
        ModuleBase[] modules = TypeLoader
            .LoadAssembliesTypesByBase("LogAnalyzer.Modules", typeof(ModuleBase))
            .Select(static type => (ModuleBase)Activator.CreateInstance(type)!)
            .ToArray();

        PerformDependencyInjection(modules);
        ReactToDependencyInjection(modules);

        return modules;
    }

    private static void PerformDependencyInjection(ModuleBase[] modules)
    {
        IServiceCollection services = new ServiceCollection();

        foreach (ModuleBase module in modules)
        {
            if (module is IDependencyInjectionModule dependencyModule)
                dependencyModule.RegisterDependencies(services);
        }

        foreach (ModuleBase module in modules)
            module.SetServiceProvider(services.BuildServiceProvider(true));
    }

    private static void ReactToDependencyInjection(ModuleBase[] modules)
    {
        foreach (ModuleBase module in modules)
        {
            if (module is IReactToDIModule reactToDiModule)
                reactToDiModule.OnDIFinished();
        }
    }
}